using System.Linq;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using LibreriaClassi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comunicazioni.Controllers
{
    public class ComunicazioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private object logger;

        public ComunicazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //----------------------------------------------//
        //LIST------------------------------------------//
        //----------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> List()
        {
            string ruolo = HttpContext.Session.GetString("ruolo");
            List<IGrouping<Guid, Comunicazione>> comunicazioni;

            if (ruolo == "Studente")
            {
                var studente_chiave = Guid.Parse(HttpContext.Session.GetString("chiave"));

                comunicazioni = await dbContext.Comunicazioni
                    .Where(c => c.K_Studente == studente_chiave || c.K_Soggetto == studente_chiave)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                foreach (var gruppo in comunicazioni)
                {
                    foreach (var comunicazione in gruppo)
                    {
                        // Carica il mittente (se è uno studente)
                        if (comunicazione.K_Soggetto.HasValue)
                        {
                            comunicazione.Studente = await dbContext.Studenti
                                .FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        }
                        // Carica il mittente (se è un docente) - Potrebbe essere necessario un controllo aggiuntivo se K_Soggetto può essere sia studente che docente
                        if (comunicazione.K_Soggetto.HasValue && comunicazione.Studente == null)
                        {
                            comunicazione.Docente = await dbContext.Docenti
                                .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                        }
                    }
                }

                return View(comunicazioni);
            }
            // ... Logica simile per il ruolo "Docente" e "Altro" ...
            else if (ruolo == "Docente")
            {
                var docente_chiave = Guid.Parse(HttpContext.Session.GetString("chiave"));

                comunicazioni = await dbContext.Comunicazioni
                    .Where(c => c.K_Docente == docente_chiave || c.K_Soggetto == docente_chiave)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                foreach (var gruppo in comunicazioni)
                {
                    foreach (var comunicazione in gruppo)
                    {
                        // Carica il mittente (se è un docente)
                        if (comunicazione.K_Soggetto.HasValue)
                        {
                            comunicazione.Docente = await dbContext.Docenti
                                .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                        }
                        // Carica il mittente (se è uno studente) 
                        if (comunicazione.K_Soggetto.HasValue && comunicazione.Docente == null)
                        {
                            comunicazione.Studente = await dbContext.Studenti
                                .FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        }
                    }
                }

                return View(comunicazioni);
            }
            else
            {

                // Recupera i Codice_Comunicazione dei messaggi inviati dall'amministrazione
                var codiciComunicazioneAmministrazione = await dbContext.Comunicazioni
                    .Where(c => c.K_Studente == null && c.K_Docente == null)
                    .Select(c => c.Codice_Comunicazione)
                    .Distinct()
                    .ToListAsync();

                // Recupera tutte le comunicazioni che hanno un Codice_Comunicazione appartenente alle comunicazioni amministrative
                comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Where(c => codiciComunicazioneAmministrazione.Contains(c.Codice_Comunicazione)) // Qui prendiamo sia le comunicazioni amministrative che le risposte
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                foreach (var gruppo in comunicazioni)
                {
                    foreach (var comunicazione in gruppo)
                    {
                        // Carica il mittente (se è un docente)
                        if (comunicazione.K_Soggetto.HasValue)
                        {
                            comunicazione.Docente = await dbContext.Docenti
                                .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                        }
                        // Carica il mittente (se è uno studente) 
                        if (comunicazione.K_Soggetto.HasValue && comunicazione.Docente == null)
                        {
                            comunicazione.Studente = await dbContext.Studenti
                                .FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        }
                    }
                }

                return View(comunicazioni);

            }

        }

        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        public void PopolaEsami(Guid? IDEsame)
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami
                .Where(e => e.K_Docente == Guid.Parse(HttpContext.Session.GetString("chiave")))

                .Select(i => new SelectListItem
                {
                    Text = i.TitoloEsame,
                    Value = i.K_Esame.ToString()

                .Select(e => new SelectListItem
                {
                    Text = e.TitoloEsame,
                    Value = e.K_Esame.ToString(),
                    Selected = (IDEsame.HasValue && e.K_Esame == IDEsame.Value)

                });
            ViewBag.EsamiList = listaEsami;
        }

        public void PopolaStudenti(Guid? K_Esame)
        {
            string ruolo = HttpContext.Session.GetString("ruolo");
            if (ruolo == "Operatore")
            {
                IEnumerable<SelectListItem> listaStudenti = dbContext.Studenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Studente.ToString()
                    });
                ViewBag.StudentiList = listaStudenti;
            }
            else if (ruolo == "Docente" && K_Esame.HasValue)
            {
                var pianiDiStudio = dbContext.PianiStudioPersonali
                .Where(ps => ps.K_Esame == K_Esame.Value)
                .Select(ps => ps.K_Studente); // Ottieni solo gli ID degli studenti

                // Recupera gli studenti che hanno un K_Studente presente nei piani di studio trovati
                IEnumerable<SelectListItem> listaStudenti = dbContext.Studenti
                    .Where(s => pianiDiStudio.Contains(s.K_Studente))
                    .Select(s => new SelectListItem
                    {
                        Text = s.Nome + " " + s.Cognome,
                        Value = s.K_Studente.ToString()
                    });
                ViewBag.StudentiList = listaStudenti;
            }
        }

        public void PopolaDocenti()
        {
            string ruolo = HttpContext.Session.GetString("ruolo");
            if (ruolo == "Operatore")
            {
                IEnumerable<SelectListItem> listaDocenti = dbContext.Docenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Docente.ToString()
                    });
                ViewBag.DocentiList = listaDocenti;
            }
            else if (ruolo == "Studente")
            {
                var Idstudente = Guid.Parse(HttpContext.Session.GetString("chiave"));
                var pianiDiStudio = dbContext.PianiStudioPersonali
                .Where(ps => ps.K_Studente == Idstudente)
                .Select(ps => ps.K_Esame);

                var listaDocenti = dbContext.Esami
                       .Where(e => pianiDiStudio.Contains(e.K_Esame))
                       .Select(e => e.Docente)
                       .Distinct()
                       .Select(docente => new SelectListItem
                       {
                           Text = docente.Nome + " " + docente.Cognome,
                           Value = docente.K_Docente.ToString()
                       }).ToList();  // Converti in lista

                ViewBag.DocentiList = listaDocenti;
            }
        }


        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            PopolaEsami(null);
            PopolaStudenti(null);
            PopolaDocenti();
            return View();
        }

        [HttpPost]
        public IActionResult AddStudente(Guid? K_Esame) // Riceve l'ID dell'esame selezionato
        {
            PopolaEsami(null);
            PopolaStudenti(K_Esame);
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddComunicazioneViewModel viewModel)
        {

            string ruolo = HttpContext.Session.GetString("ruolo");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = Guid.NewGuid(),
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave")),
                K_Studente = viewModel.K_Studente,
                K_Docente = viewModel.K_Docente
            };

            if (ruolo == "Operatore")
            {
                comunicazione.Soggetto = "A";
                //aggiunta necessaria: di default Guid? = 00000000-0000-0000-0000-0000-0000-0000, quindi non risultava null,
                // e il meccanismo di list si inceppava

                if (comunicazione.K_Studente != null)
                {
                    comunicazione.K_Docente = null;
                }
                else if (comunicazione.K_Docente != null)
                {
                    comunicazione.K_Studente = null;
                }
            }
            else if (ruolo == "Docente")
            {
                comunicazione.K_Docente = null;
                comunicazione.Soggetto = "D";
                if (viewModel.K_Studente == null || viewModel.K_Studente.ToString() == "Amministrazione")  // Se "Amministrazione"
                {
                    comunicazione.K_Studente = null;  // Non associato a uno studente
                }
            }
            else if (ruolo == "Studente")
            {
                comunicazione.K_Studente = null;
                comunicazione.Soggetto = "S";
                if (viewModel.K_Docente == null || viewModel.K_Docente.ToString() == "Amministrazione")  // Se "Amministrazione"
                {
                    comunicazione.K_Docente = null;  // Non associato a uno studente
                }
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();

            //chiariamo il ruolo e identifichiamo i relativi destinatari 
            //LIST vuota da riempire con i destinatari 
            List<string> destinatari = new List<string>();

            //Controllo del ruolo 
            //se ruolo è nullo o non corrisponde a uno di quelli considerati "RUOLO NON VALIDO"
            if (string.IsNullOrEmpty(ruolo) || !new[] { "Docente", "Studente", "Operatore" }.Contains(ruolo))
            {
                Console.WriteLine("Errore: ruolo non valido.");
                return Content("Ruolo non valido."); // Interrompe l'esecuzione del metodo
                /* return HttpNotFound();  // Restituisce un errore 404*/
            }

            if (ruolo == "Docente")
            {
                if (comunicazione.K_Esame != null)
                {
                    //Recupera la lista di studenti iscritti all'esame
                    //Se non ci sono piani studio associati all'esame, viene restituito un errore.
                    var pianiStudio = dbContext.PianiStudioPersonali.Where(p => p.K_Esame == comunicazione.K_Esame).ToList();
                    if (!pianiStudio.Any())
                    {
                        Console.WriteLine("Nessun piano studio trovato per l'esame.");
                        return Content("Ruolo non valido.");
                        /* return HttpNotFound();  // Restituisce un errore 404*/
                    }

                    var studenti = pianiStudio.Select(p => p.K_Studente).Distinct().ToList();

                    // Estrai le email degli studenti dal db se ci sono studenti iscritti
                    foreach (var studenteId in studenti)
                    {
                        var studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);
                        if (studente != null && !string.IsNullOrEmpty(studente.Email))
                        {
                            // Usa Guid.TryParse per evitare il problema di parsing
                            Guid guid;
                            if (Guid.TryParse(studente.Email, out guid))
                            {
                                // Se il parsing ha successo, aggiungi l'email
                                destinatari.Add(studente.Email);
                            }
                            else
                            {
                                // Gestisci caso dove l'email non è un GUID valido (se necessario)
                                Console.WriteLine($"Attenzione: L'email dello studente '{studente.Email}' non è valida. Studente ID: {studente.K_Studente}");
                            }
                        }
                    }

                    //Recupera le email degli operatori e aggiungi alla lista 
                    var operatori = dbContext.Operatori.ToList();
                    foreach (var operatore in operatori)
                    {
                        if (operatore != null && !string.IsNullOrEmpty(operatore.USR))
                        {
                            destinatari.Add(operatore.USR);
                        }
                    }
                }
            }
            else if (ruolo == "Studente")
            {
                if (comunicazione.K_Esame != null)
                {
                    //Recupera l'email del docente
                    var docente = dbContext.Esami.FirstOrDefault(e => e.K_Esame == comunicazione.K_Esame)?.Docente?.Email;
                    if (!string.IsNullOrEmpty(docente))
                    {
                        // Usa Guid.TryParse anche per il docente se necessario
                        Guid guid;
                        if (Guid.TryParse(docente, out guid))
                        {
                            //Se il parsing ha successo, aggiungi l'email
                            destinatari.Add(docente);
                        }
                        else
                        {
                            // Gestisci caso dove l'email del docente non è un GUID valido (se necessario)
                            Console.WriteLine($"Attenzione: L'email del docente '{docente}' non è valida. Esame ID: {comunicazione.K_Esame}");
                        }
                    }
                }

                // Aggiungi gli studenti iscritti al corso (escluso lo studente che invia)
                var studenti = dbContext.Studenti.Where(s => s.K_Corso == comunicazione.K_Esame && s.K_Studente != comunicazione.K_Studente).ToList();
                foreach (var studente in studenti)
                {
                    if (!string.IsNullOrEmpty(studente.Email))
                    {
                        // Usa Guid.TryParse per evitare il problema di parsing
                        Guid guid;
                        if (Guid.TryParse(studente.Email, out guid))
                        {
                            // Se il parsing ha successo, aggiungi l'email
                            destinatari.Add(studente.Email);
                        }
                        else
                        {
                            // Gestisci caso dove l'email non è un GUID valido (se necessario)
                            Console.WriteLine($"Attenzione: L'email dello studente '{studente.Email}' non è valida. Studente ID: {studente.K_Studente}");
                        }
                    }
                }

                // Aggiungi gli operatori
                var operatori = dbContext.Operatori.ToList();
                foreach (var operatore in operatori)
                {
                    if (!string.IsNullOrEmpty(operatore.USR))
                    {
                        destinatari.Add(operatore.USR);
                    }
                }
            }

            else if (ruolo == "Operatore")
            {
                if (comunicazione.K_Studente == null && comunicazione.K_Docente == null)
                {
                    // Aggiungi tutte le email di studenti e docenti
                    var studenti = dbContext.Studenti.ToList();
                    foreach (var studente in studenti)
                    {
                        if (!string.IsNullOrEmpty(studente.Email))
                        {
                            // Usa Guid.TryParse se necessario per l'email
                            Guid guid;
                            if (Guid.TryParse(studente.Email, out guid))
                            {
                                destinatari.Add(studente.Email);
                            }
                        }
                    }

                    var docenti = dbContext.Docenti.ToList();
                    foreach (var docente in docenti)
                    {
                        if (!string.IsNullOrEmpty(docente.Email))
                        {
                            // Usa Guid.TryParse per il docente se necessario
                            Guid guid;
                            if (Guid.TryParse(docente.Email, out guid))
                            {
                                destinatari.Add(docente.Email);
                            }
                        }
                    }
                }
                else
                {
                    if (comunicazione.K_Studente != null)
                    {
                        var studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente);
                        if (studente != null && !string.IsNullOrEmpty(studente.Email))
                        {
                            // Usa Guid.TryParse per l'email dello studente se necessario
                            Guid guid;
                            if (Guid.TryParse(studente.Email, out guid))
                            {
                                destinatari.Add(studente.Email);
                            }
                        }
                    }

                    if (comunicazione.K_Docente != null)
                    {
                        var docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente);
                        if (docente != null && !string.IsNullOrEmpty(docente.Email))
                        {
                            // Usa Guid.TryParse per l'email del docente se necessario
                            Guid guid;
                            if (Guid.TryParse(docente.Email, out guid))
                            {
                                destinatari.Add(docente.Email);
                            }
                        }
                    }
                }
            }



            //invia email

            //Se la lista ha destinatari viene creato un oggetto SmtpClient per inviare l'email tramite un server SMTP (mail.brovia.it).
            //Per ogni destinatario nella lista destinatari, viene aggiunto l'indirizzo email a mail.To.
            //Il corpo dell'email è il testo della comunicazione (comunicazione.Testo), e l'oggetto è "Nuova comunicazione".
            //Infine, si tenta di inviare l'email con il metodo smtpClient.Send(mail).
            if (destinatari.Any())
            {
                SmtpClient smtpClient = new SmtpClient("mail.brovia.it", 25);
                /*smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");*/
                smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");
                smtpClient.EnableSsl = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("generation@brovia.it", "Sistema Comunicazioni");

                // Aggiungi destinatari dalla lista
                foreach (var email in destinatari.Distinct())
                {
                    mail.To.Add(new MailAddress(email));
                }

                // Aggiungi anche il tuo indirizzo email per il test
                mail.To.Add(new MailAddress("mffrso99@gmail.com"));  // Modifica con il tuo indirizzo

                mail.Subject = "Nuova comunicazione";
                mail.Body = comunicazione.Testo;

                try
                {
                    smtpClient.Send(mail); //Tentativo di inviare l'email
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore invio email: " + ex.Message); // Stampa l'errore sulla console
                }
            }



            return RedirectToAction("List", "Comunicazioni");

        }





        [HttpPost]
        [HttpPost]

        public async Task<IActionResult> AddRisposta(Comunicazione viewModel)
        {
            string ruolo = HttpContext.Session.GetString("ruolo");
            Guid chiaveUtente = Guid.Parse(HttpContext.Session.GetString("chiave"));

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
                Testo = viewModel.Testo,
                K_Soggetto = chiaveUtente,
            };

            if (ruolo == "Operatore")
            {
                nuovaRisposta.Soggetto = "A";
            }
            else if (ruolo == "Docente")
            {
                nuovaRisposta.Soggetto = "D";
                nuovaRisposta.K_Studente = ultimaComunicazione.K_Studente;
            }
            else if (ruolo == "Studente")
            {
                nuovaRisposta.Soggetto = "S";
                nuovaRisposta.K_Docente = ultimaComunicazione.K_Docente;
            }

            // Assegna chi riceverà la risposta
            nuovaRisposta.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == nuovaRisposta.K_Docente);
            nuovaRisposta.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == nuovaRisposta.K_Studente);

            await dbContext.Comunicazioni.AddAsync(nuovaRisposta);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Comunicazioni");
        }



    }
}

