using AreaStudente.Data;
using AreaStudente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace AreaStudente.Controllers
{

    public class StudentiController : Controller
    {



        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }

        //public IActionResult LoginRedirect()


        //{
        //    // Leggi parametri dalla query string e salvali in sessione
        //    var usr = Request.Query["usr"];
        //    var guidid = Request.Query["guidid"];
        //    var tipo = Request.Query["tipo"];

        //    if (!string.IsNullOrEmpty(usr) && !string.IsNullOrEmpty(guidid) && !string.IsNullOrEmpty(tipo))
        //    {
        //        HttpContext.Session.SetString("usr", usr);
        //        HttpContext.Session.SetString("guidid", guidid);
        //        HttpContext.Session.SetString("tipo", tipo);
        //    }
        //    else
        //    {
        //        return BadRequest("Parametri mancanti o invalidi.");
        //    }

        //    // Reindirizza all'area studente dopo aver settato la sessione
        //    return RedirectToAction("Show", "Studenti");
        //}

        [HttpGet]
        public async Task<IActionResult> Show(Guid id, Comunicazione c) // L'ID dello studente da visualizzare
        {

            // Trova lo studente includendo potenzialmente dati correlati se servissero
            // In questo caso, per il ViewModel fornito, non serve caricare il Corso,
            // ma lo lascio commentato come esempio se volessi il nome del corso in futuro.
            var studente = await dbContext.Studenti
                                          // Sostituisci con il tuo DbSet<Studente>
                                          // .Include(s => s.Corso) // Esempio: Decommenta se hai una navigation property 'Corso' in Studente e vuoi il nome
                                          .FirstOrDefaultAsync(s => s.K_Studente == id);
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
            ViewData["matricola"] = studente.Matricola;
            HttpContext.Session.SetString("studente_id", studente.K_Studente.ToString().ToUpper());
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
                Abilitato = studente.Abilitato



                // Opzione 3: Se la stringa nel DB ha valori specifici come "ATTIVO", "SOSPESO" etc.
                //            e vuoi mapparli a "Sì"/"No" o altro. Esempio:
                // Abilitato = studente.Abilitato?.ToUpper() switch
                // {
                //     "ATTIVO" => "Sì",
                //     "TRUE" => "Sì",
                //     "1" => "Sì",
                //     null => "Non specificato",
                //     _ => "No" // Tutti gli altri casi ("SOSPESO", "FALSE", "0", etc.)
                // },
            };

            var comunicazioni = await dbContext.Comunicazioni
                .Where(c => c.K_Studente == studente.K_Studente || c.K_Soggetto == studente.K_Studente)
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
                Comunicazioni = comunicazioni
            };

            return View(dashboardViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> ModificaProfilo(Guid id)
        {
            ViewData["studente_id"] = id;
            var studente = await dbContext.Studenti
                 .FirstOrDefaultAsync(s => s.K_Studente == id);

            if (studente == null)
            {
                TempData["PopupErrore"] = "Studente non trovato.";
                return RedirectToAction("Index", "Home");
            }

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

                ImmagineProfilo = studente.ImmagineProfilo
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ModificaProfilo(ModificaStudenteViewModel model, string PasswordNew, string PasswordConfirm, Guid id)
        {
            ViewData["studente_id"] = id;
            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == model.K_Studente);



            if (studente == null)
                return NotFound();

            // Aggiorna i dati anagrafici
            studente.Nome = model.Nome;
            studente.Cognome = model.Cognome;
            studente.Indirizzo = model.Indirizzo;
            studente.CAP = model.CAP;
            studente.Citta = model.Citta;
            studente.Provincia = model.Provincia;
            studente.DataNascita = model.DataNascita;

            if (model.ImmagineProfiloFile != null && model.ImmagineProfiloFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImmagineProfiloFile.CopyToAsync(memoryStream);
                    studente.ImmagineProfilo = memoryStream.ToArray();
                    studente.Tipo = model.ImmagineProfiloFile.ContentType;
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

                    return RedirectToAction("ModificaProfilo");
                }

                // 2. Password vecchia errata


                if (model.PWD != studente.PWD)
                {
                    TempData["PopupErrore"] = "La password vecchia inserita non risulta essere corretta.";
                    TempData["ApriModalePassword"] = true;

                    return RedirectToAction("ModificaProfilo");
                }

                // 3. Password nuova e conferma non coincidono
                if (PasswordNew != PasswordConfirm)
                {
                    TempData["PopupErrore"] = "La nuova password e la conferma non coincidono.";
                    TempData["ApriModalePassword"] = true;
                    return RedirectToAction("ModificaProfilo");
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
            return RedirectToAction("ModificaProfilo", "Studenti");
        }



    }



}




