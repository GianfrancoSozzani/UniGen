using AreaStudente.Data;
using AreaStudente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace AreaStudente.Controllers
{
    
    public class StudentiController : Controller
    {



        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }

        public IActionResult LoginRedirect()


        {
            // Leggi parametri dalla query string e salvali in sessione
            var usr = Request.Query["usr"];
            var guidid = Request.Query["guidid"];
            var tipo = Request.Query["tipo"];

            if (!string.IsNullOrEmpty(usr) && !string.IsNullOrEmpty(guidid) && !string.IsNullOrEmpty(tipo))
            {
                HttpContext.Session.SetString("usr", usr);
                HttpContext.Session.SetString("guidid", guidid);
                HttpContext.Session.SetString("tipo", tipo);
            }
            else
            {
                return BadRequest("Parametri mancanti o invalidi.");
            }

            // Reindirizza all'area studente dopo aver settato la sessione
            return RedirectToAction("Show", "Studenti");
        }

        [HttpGet]
        public async Task<IActionResult> Show() // L'ID dello studente da visualizzare
        {
            var guididStr = HttpContext.Session.GetString("guidid");
            if (!Guid.TryParse(guididStr, out Guid guidid))
            {
                return RedirectToAction("LoginRedirect");
            }

            var studente = await dbContext.Studenti
                .FirstOrDefaultAsync(s => s.K_Studente == guidid);
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
                .Where(c => c.K_Studente == studente.K_Studente)
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
                .ToListAsync();


            var dashboardViewModel = new StudenteDashboardViewModel
            {
                Studente = viewModel,
                Comunicazioni = comunicazioni
            };

            return View(dashboardViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> ModificaProfilo()
        {
            var guididStr = HttpContext.Session.GetString("guidid");
            if (!Guid.TryParse(guididStr, out Guid guidid))
            {
                return RedirectToAction("LoginRedirect");
            }

            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == guidid);

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
        public async Task<IActionResult> ModificaProfilo(ModificaStudenteViewModel model, string PasswordNew, string PasswordConfirm)
        {
            var guididStr = HttpContext.Session.GetString("guidid");
            if (!Guid.TryParse(guididStr, out Guid guidid) || guidid != model.K_Studente)
            {
                return RedirectToAction("LoginRedirect");
            }

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

        public IActionResult Recupera()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recupera(RecuperaViewModel model, string Email)
        {
            var mail = await dbContext.Studenti.FirstOrDefaultAsync(s => s.Email == model.Email);
            if (mail == null)
            {
                TempData["PopupErrore"] = "Questa mail non risulta essere registrata";
                return RedirectToAction("Recupera");
            }
            TempData["PopupSuccesso"] = "Controlla la tua Mail, ti abbiamo inviato un link per recuperare la Password";
            return RedirectToAction("Recupera", "Studenti");

        }


    }



}


 

