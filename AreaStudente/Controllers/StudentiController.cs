using AreaStudente.Data;
using AreaStudente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AreaStudente.Models.Entities;
using System.Data;
using System.Net.Mail;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;


namespace AreaStudente.Controllers
{

    public class StudentiController : Controller
    {



        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }

        [HttpGet]

        public async Task<IActionResult> Show(Guid cod, string usr, string r) // L'ID dello studente da visualizzare

        {
            //Comunicazione c;
            var studente = await dbContext.Studenti
                                          // Sostituisci con il tuo DbSet<Studente>
                                          .Include(s => s.Corso)
                                          .ThenInclude(c => c.Facolta)
                                          .FirstOrDefaultAsync(s => s.K_Studente == cod);
            if (studente == null)
            {
                ViewBag.ErrorMessage = $"Nessun dato studente da visualizzare.Assicurati di aver specificato un ID valido.";

                // Torni comunque alla view "Show" passando un model vuoto
                return View(new StudenteDashboardViewModel
                {
                    Studente = new ShowStudenteViewModel(),
                    Comunicazioni = new List<ComunicazioneViewModel>()
                });
            }

            ViewData["studente_id"] = studente.K_Studente;
            ViewData["email"] = studente.Email;
            ViewData["matricola"] = studente.Matricola;
            ViewData["abilitato"] = studente.Abilitato;
            ViewData["ruolo"] = "s";
            HttpContext.Session.SetString("cod", studente.K_Studente.ToString());
            HttpContext.Session.SetString("r", "s");

            // Mappa dall'entità Studente (dal DB) a ShowStudenteViewModel
            // Dentro l'action Show() nel StudentiController.cs, dopo aver recuperato 'studente'

            var viewModel = new ShowStudenteViewModel
            {
                K_Studente = studente.K_Studente,
                Email = studente.Email,
                Cognome = studente.Cognome,
                Nome = studente.Nome,
                DataNascita = studente.DataNascita,
                Indirizzo = studente.Indirizzo,
                CAP = studente.CAP,
                Citta = studente.Citta,
                Provincia = studente.Provincia,
                ImmagineProfilo = studente.ImmagineProfilo,
                Tipo = studente.Tipo,
                Matricola = studente.Matricola,
                DataImmatricolazione = studente.DataImmatricolazione,
                K_Corso = studente.K_Corso,
                Abilitato = studente.Abilitato,
                CorsoTitolo = studente.Corso?.TitoloCorso,
                FacoltaTitolo = studente.Corso?.Facolta?.TitoloFacolta,
            };

            var comunicazioni = await dbContext.Comunicazioni
                .Where(c => c.K_Soggetto == studente.K_Studente && c.K_Docente == null || c.K_Studente == studente.K_Studente && dbContext.Operatori.Any(o => o.K_Operatore == c.K_Soggetto))
                .Select(c => new ComunicazioneViewModel
                {
                    K_Comunicazione = c.K_Comunicazione,
                    Codice_Comunicazione = c.Codice_Comunicazione,
                    DataOraComunicazione = c.DataOraComunicazione,
                    Soggetto = c.Soggetto,
                    K_Soggetto = c.K_Soggetto,
                    Testo = c.Testo,
                    K_Studente = c.K_Studente
                })
                .OrderBy(c => c.DataOraComunicazione)
                .ToListAsync();

            var comunicazioniGruppo = comunicazioni
            .GroupBy(c => c.Codice_Comunicazione)
            .ToList();

            foreach (var gruppo in comunicazioniGruppo)
            {
                foreach (var comunicazione in gruppo)
                {
                    if (comunicazione.K_Soggetto.HasValue)
                    {
                        var studentesogg = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        if (studentesogg != null)
                        {
                            comunicazione.Studente = studentesogg;
                        }
                        else
                        {
                            var docente = await dbContext.Docenti.FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                            if (docente != null)
                            {
                                comunicazione.Docente = docente;
                            }
                        }
                    }
                }
            }



            var dashboardViewModel = new StudenteDashboardViewModel
            {
                Studente = viewModel,
                Comunicazioni = comunicazioni,
                NuovaComunicazione = new AddComunicazioneViewModel()
            };

            return View(dashboardViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRisposta(Comunicazione viewModel)
        {

            ViewData["studente_id"] = HttpContext.Session.GetString("cod");
            Guid studente_id = new Guid(HttpContext.Session.GetString("cod"));

            string ruolo = HttpContext.Session.GetString("r");


            var ultimaComunicazione = dbContext.Comunicazioni
           .Where(c => c.Codice_Comunicazione == viewModel.Codice_Comunicazione)
           .OrderByDescending(c => c.DataOraComunicazione)
           .FirstOrDefault();

            if (ultimaComunicazione == null)
            {
                return BadRequest("Comunicazione non trovata.");
            }

            var nuovaRisposta = new Comunicazione
            {
                Codice_Comunicazione = viewModel.Codice_Comunicazione,
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo.Trim(),
                K_Soggetto = studente_id,
            };

            if (ruolo == "a")
            {
                nuovaRisposta.Soggetto = "A";

                // Mantiene il destinatario originale della conversazione
                if (ultimaComunicazione.K_Studente != null)
                {
                    nuovaRisposta.K_Studente = ultimaComunicazione.K_Studente;
                }
                else if (ultimaComunicazione.K_Docente != null)
                {
                    nuovaRisposta.K_Docente = ultimaComunicazione.K_Docente;
                }
            }


            else if (ruolo == "d")
            {
                nuovaRisposta.Soggetto = "D";
                nuovaRisposta.K_Studente = ultimaComunicazione.K_Studente;
            }
            else if (ruolo == "s")
            {
                nuovaRisposta.Soggetto = "S";
                nuovaRisposta.K_Docente = ultimaComunicazione.K_Docente;
            }

            // Assegna chi riceverà la risposta
            nuovaRisposta.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == nuovaRisposta.K_Docente);
            nuovaRisposta.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == nuovaRisposta.K_Studente);

            await dbContext.Comunicazioni.AddAsync(nuovaRisposta);
            await dbContext.SaveChangesAsync();

            /// LOGICA EMAIL: invio dell'email per la risposta
            List<string> destinatariEmail = new List<string>();

            if (ruolo == "d")
            {
                // Se il ruolo è docente, invia allo studente
                if (nuovaRisposta.K_Studente.HasValue && nuovaRisposta.Studente?.Email != null)
                {
                    destinatariEmail.Add(nuovaRisposta.Studente.Email);
                }
                else
                {
                    destinatariEmail.Add("generation@brovia.it"); // Default admin email
                }
            }
            else if (ruolo == "s")
            {
                // Se il ruolo è studente, invia al docente
                if (nuovaRisposta.K_Docente.HasValue && nuovaRisposta.Docente?.Email != null)
                {
                    destinatariEmail.Add(nuovaRisposta.Docente.Email);
                }
                else
                {
                    destinatariEmail.Add("generation@brovia.it"); // Default admin email
                }
            }
            else
            {
                // Se il ruolo è amministratore o altro, invia a tutti
                destinatariEmail.Add("generation@brovia.it");
            }

            // Invia email se ci sono destinatari
            if (destinatariEmail.Any())
            {
                SmtpClient smtpClient = new SmtpClient("mail.brovia.it", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");
                smtpClient.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("generation@brovia.it", "Comunicazione UniGen");

                foreach (var email in destinatariEmail.Distinct())
                {
                    mail.To.Add(new MailAddress(email));
                }

                mail.Subject = "Nuova risposta alla tua comunicazione";
                mail.Body = @$"In data {nuovaRisposta.DataOraComunicazione}  
                hai ricevuto una risposta a una comunicazione precedente. 

                {nuovaRisposta.Testo}"; // Testo della nuova risposta

                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore invio email: " + ex.Message);
                }
            }

            return RedirectToAction("Show", "Studenti", new { cod = HttpContext.Session.GetString("cod") });
        }


        [HttpGet]
        public async Task<IActionResult> ModificaProfilo(Guid cod)
        {
            ViewData["studente_id"] = cod;
            var studente = await dbContext.Studenti
                 .Include(s => s.Corso)
                 .ThenInclude(c => c.Facolta)
                 .FirstOrDefaultAsync(s => s.K_Studente == cod);


            if (studente == null)
            {
                TempData["PopupErrore"] = "Studente non trovato.";
                return RedirectToAction("Index", "Home");
            }

            ViewData["email"] = studente.Email;
            ViewData["matricola"] = studente.Matricola;
            ViewData["abilitato"] = studente.Abilitato;

            var model = new ModificaStudenteViewModel
            {
                K_Studente = studente.K_Studente,
                Nome = studente.Nome,
                Cognome = studente.Cognome,
                Email = studente.Email,
                DataNascita = studente.DataNascita,
                Indirizzo = studente.Indirizzo,
                CAP = studente.CAP,
                Citta = studente.Citta,
                Provincia = studente.Provincia,
                ImmagineProfilo = studente.ImmagineProfilo,
                Matricola = studente.Matricola,
                DataImmatricolazione = studente.DataImmatricolazione,
                Corso = studente.Corso,
                //= studente.Abilitato
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> ModificaProfilo(ModificaStudenteViewModel model, string PasswordNew, string PasswordConfirm, Guid cod)

        {
            ViewData["studente_id"] = cod;
            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == model.K_Studente);

            if (studente == null)
                return NotFound();

            ViewData["email"] = studente.Email;
            ViewData["matricola"] = studente.Matricola;
            ViewData["abilitato"] = studente.Abilitato;

            // Aggiorna i dati anagrafici
            studente.Nome = CapitalizeWords(model.Nome);
            studente.Cognome = CapitalizeWords(model.Cognome);
            studente.Indirizzo = model.Indirizzo;
            studente.CAP = model.CAP;
            studente.Citta = model.Citta;
            studente.Provincia = model.Provincia.ToUpper();
            studente.DataNascita = model.DataNascita;

            if (model.ImmagineProfiloFile != null && model.ImmagineProfiloFile.Length > 0)
            {
                var tipo = model.ImmagineProfiloFile.ContentType.ToLower();
                if (tipo != "image/jpeg" && tipo != "image/jpg" && tipo != "image/png")
                {
                    TempData["AlertMessage"] = "Formato non valido. Sono accettati solo JPG, JPEG e PNG.";
                    return RedirectToAction("ModificaProfilo", "Studenti", new { cod = model.K_Studente });
                }

                using (var memoryStream = new MemoryStream())
                {
                    await model.ImmagineProfiloFile.CopyToAsync(memoryStream);
                    studente.ImmagineProfilo = memoryStream.ToArray();
                    studente.Tipo = tipo;
                }

                TempData["PopupSuccesso"] = "Immagine aggiornata con successo.";
            }

            //logica password
            bool AlmenoUnoCompilato = !string.IsNullOrEmpty(model.PWD) || !string.IsNullOrEmpty(PasswordNew) || !string.IsNullOrEmpty(PasswordConfirm);
            bool tuttiCompilati = !string.IsNullOrEmpty(model.PWD) && !string.IsNullOrEmpty(PasswordNew) && !string.IsNullOrEmpty(PasswordConfirm);


            if (AlmenoUnoCompilato)
            {
                // 1. Manca almeno un campo
                if (!tuttiCompilati)
                {
                    TempData["PopupErrore"] = "Per cambiare la password, devi compilare tutti e tre i campi.";

                    TempData["ApriModalePassword"] = true;

                    return RedirectToAction("ModificaProfilo", "Studenti", new { cod = model.K_Studente });
                }

                // 2. Password vecchia errata


                if (model.PWD != studente.PWD)
                {
                    TempData["PopupErrore"] = "La password vecchia inserita non risulta essere corretta.";
                    TempData["ApriModalePassword"] = true;

                    return RedirectToAction("ModificaProfilo", "Studenti", new { cod = model.K_Studente });
                }

                // 3. Password nuova e conferma non coincidono
                if (PasswordNew != PasswordConfirm)
                {
                    TempData["PopupErrore"] = "La nuova password e la conferma non coincidono.";
                    TempData["ApriModalePassword"] = true;
                    return RedirectToAction("ModificaProfilo", "Studenti", new { cod = model.K_Studente });
                }

                // 4. Tutto corretto, aggiorna
                TempData["PopupErrore"] = null;
                TempData["ApriModalePassword"] = true;
                TempData["PopupSuccesso"] = "Password aggiornata con successo.";
                studente.PWD = PasswordNew;
            }


            await dbContext.SaveChangesAsync();
            TempData["DisplaySuccessMsg"] = true;
            TempData["PopupSuccesso"] = "I dati sono stati salvati correttamente.";
            return RedirectToAction("ModificaProfilo", "Studenti", new { cod = model.K_Studente });
        }

        private string CapitalizeWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return string.Join(" ",
                input.Trim().ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                     .Select(word => char.ToUpper(word[0]) + word.Substring(1))
            );
        }

        [HttpGet]
        public async Task<IActionResult> Immatricolati(Guid cod, Guid K_Facolta)
        {
            ViewData["studente_id"] = cod;
            var studente = await dbContext.Studenti
                .Include(s => s.Corso)
                .ThenInclude(c => c.Facolta)
                .FirstOrDefaultAsync(s => s.K_Studente == cod);
            //dibbiamo capire, per lo studente già immatricolato, come comportarci e quali dati fare visualizzare.


            if (studente == null)
            {
                TempData["PopupErrore"] = "Studente non trovato.";
                return RedirectToAction("Show", "Studenti", new { cod = HttpContext.Session.GetString("cod") });
            }

            //rispecifico i parametri per passarli ad altre pg.
            ViewData["email"] = studente.Email;
            ViewData["matricola"] = studente.Matricola;
            ViewData["abilitato"] = studente.Abilitato;

            var selectedFacolta = K_Facolta;

            var model = new ModificaStudenteViewModel
            {
                K_Studente = studente.K_Studente,
                Nome = studente.Nome,
                Cognome = studente.Cognome,
                Email = studente.Email,
                DataNascita = studente.DataNascita,
                Indirizzo = studente.Indirizzo,
                CAP = studente.CAP,
                Citta = studente.Citta,
                Provincia = studente.Provincia,
                ImmagineProfilo = studente.ImmagineProfilo,
                Matricola = studente.Matricola,
                DataImmatricolazione = studente.DataImmatricolazione,
                K_Facolta = selectedFacolta,
                K_Corso = studente.K_Corso,
                FacoltaList = PopolaFacolta(),
                CorsiList = PopolaCorsi(selectedFacolta)
                //= studente.Abilitato


            };
            


            return View(model);

        }


        private IEnumerable<SelectListItem> PopolaFacolta()
        {
            return dbContext.Facolta
                .Select(f => new SelectListItem
                {
                    Text = f.TitoloFacolta,
                    Value = f.K_Facolta.ToString()
                }).ToList();
        }

        //Questo metodo recupera i corsi legati alla facoltà specificata. Se k_facolta è nullo, restituisce una lista vuota.
        private IEnumerable<SelectListItem> PopolaCorsi(Guid? k_facolta)
        {
            if (k_facolta == null)
                return new List<SelectListItem>();

            return dbContext.Corsi
                .Where(c => c.K_Facolta == k_facolta)
                .Select(c => new SelectListItem
                {
                    Text = c.TitoloCorso,
                    Value = c.K_Corso.ToString()
                }).ToList();
        }




        [HttpPost]
        public async Task<IActionResult> Immatricolati(ModificaStudenteViewModel model, Guid cod)
        {
            ViewData["studente_id"] = cod;

            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == model.K_Studente);
            if (studente == null)
                return NotFound();

            // Submit parziale: aggiornamento solo dei corsi

            var modelp = await dbContext.Pagamenti.FirstOrDefaultAsync(p => p.K_Studente == studente.K_Studente);

            ViewData["email"] = studente.Email;
            ViewData["matricola"] = studente.Matricola;
            ViewData["abilitato"] = studente.Abilitato;

            var corso = await dbContext.Corsi.FirstOrDefaultAsync(c => c.K_Corso == model.K_Corso);
            model.Importo = corso?.CostoAnnuale / 2; // Calcolo anticipato per la view, anche se il corso è null

            if (Request.Form.ContainsKey("updateFacolta"))
            {
                model.FacoltaList = PopolaFacolta();
                model.CorsiList = PopolaCorsi(model.K_Facolta);
                model.ImmagineProfilo = studente.ImmagineProfilo;
                return View(model);
            }

            if (corso == null)
            {
                ModelState.AddModelError("", "Devi selezionare un corso per poterti immatricolare.");
                model.FacoltaList = PopolaFacolta();
                model.CorsiList = PopolaCorsi(model.K_Facolta);
                model.ImmagineProfilo = studente.ImmagineProfilo;
                return View(model);
            }



            // 🔒 Controllo: già immatricolato?
            if (studente.Abilitato == "S" &&
                (studente.K_Corso == model.K_Corso || studente.K_Corso != null))
            {
                ModelState.AddModelError("", "Risulti già immatricolato. Se desideri procedere con una nuova immatricolazione, è necessario presentare prima la rinuncia agli studi.");
                return View(model);
            }
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Generazione della matricola, se assente
            if (!studente.Matricola.HasValue)
            {
                var anno = DateTime.Now.Year.ToString(); // es: "2025"
                var count = await dbContext.Studenti
                    .CountAsync(s => s.Matricola.HasValue && s.Matricola.Value.ToString().StartsWith(anno));

                studente.Matricola = int.Parse($"{anno}{(count + 1):D4}"); // es: 20250001
            }

            // Calcolo importo rata (già assegnato sopra a model.Importo)
            decimal? importoCalcolato = model.Importo;

            // Primo pagamento
            var pagamento = new Pagamento
            {
                K_Pagamento = modelp?.K_Pagamento ?? Guid.NewGuid(),
                K_Studente = studente.K_Studente,
                DataPagamento = DateTime.Now,
                Anno = DateTime.Now.Year.ToString(),
                Importo = importoCalcolato,
                Stato = modelp?.Stato ?? "S"
            };
            await dbContext.Pagamenti.AddAsync(pagamento);

            // Secondo pagamento (futuro)
            if (pagamento.Stato == "S" && pagamento.Importo >= 0)
            {
                var pagamentoFuturo = new Pagamento
                {
                    K_Pagamento = Guid.NewGuid(),
                    K_Studente = studente.K_Studente,
                    DataPagamento = DateTime.Now.AddMonths(6),
                    Anno = DateTime.Now.AddMonths(6).Year.ToString(),
                    Importo = importoCalcolato,
                    Stato = "N"
                };
                await dbContext.Pagamenti.AddAsync(pagamentoFuturo);
            }

            // Aggiorna lo studente
            studente.Abilitato = "S";
            studente.K_Corso = model.K_Corso;
            studente.DataImmatricolazione = DateTime.Now;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Show", "Studenti", new { cod = HttpContext.Session.GetString("cod") });
        }
    }
}





