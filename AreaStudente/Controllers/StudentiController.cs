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

        [HttpGet]
        public async Task<IActionResult> Show(Guid id) // L'ID dello studente da visualizzare
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
    }
}

        [HttpPost]
        public async Task<IActionResult> ModificaProfilo(ModificaStudenteViewModel model, string PasswordNew, string PasswordConfirm)
        {
            var studenteId = new Guid("0CDDDB26-14FE-4937-91A8-ED9014654CA3");
            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == studenteId);

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
            bool AlmenoUnoCompilato = !string.IsNullOrEmpty(model.Password) || !string.IsNullOrEmpty(PasswordNew) || !string.IsNullOrEmpty(PasswordConfirm);
            bool tuttiCompilati = !string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(PasswordNew) && !string.IsNullOrEmpty(PasswordConfirm);

            if (AlmenoUnoCompilato)
            {
                // 1. Manca almeno un campo
                if (!tuttiCompilati)
                {
                    TempData["PopupErrore"] = "Per cambiare la password, devi compilare tutti e tre i campi.";
                    return RedirectToAction("ModificaProfilo");
                }

                // 2. Password vecchia errata
                if (model.Password != studente.Password)
                {
                    TempData["PopupErrore"] = "La password vecchia inserita non risulta essere corretta.";
                    return RedirectToAction("ModificaProfilo");
                }

                // 3. Password nuova e conferma non coincidono
                if (PasswordNew != PasswordConfirm)
                {
                    TempData["PopupErrore"] = "La nuova password e la conferma non coincidono.";
                    return RedirectToAction("ModificaProfilo");
                }

                // 4. Tutto corretto, aggiorna
                TempData["PopupSuccesso"] = "Password aggiornata con successo.";
                studente.Password = PasswordNew;
            }

            await dbContext.SaveChangesAsync();
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


 


