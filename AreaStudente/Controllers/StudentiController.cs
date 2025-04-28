using AreaStudente.Data;
using AreaStudente.Models;
using AreaStudente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaStudente.Controllers
{
    public class StudentiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ModificaProfilo()
        {
            // Simulo l'ID, solo per test (poi userò la sessione)
            var studenteId = new Guid("78FF09CF-B90A-4F17-BF58-D0D6E89BFEA8");
            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.K_Studente == studenteId);

            if (studente == null)
                return NotFound();

            var model = new ModificaStudenteViewModel
            {
                Email = studente.Email,
                Nome = studente.Nome,
                Cognome = studente.Cognome,
                Indirizzo = studente.Indirizzo,
                CAP = studente.CAP,
                Citta = studente.Citta,
                Provincia = studente.Provincia,
                DataNascita = studente.DataNascita
            };
            //In questo caso il nome della view viene dedotto dall'action del controller (che ha lo stesso nome).
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModificaProfilo(ModificaStudenteViewModel model, string PasswordNew, string PasswordConfirm)
        {
            var studenteId = new Guid("78FF09CF-B90A-4F17-BF58-D0D6E89BFEA8");
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


    }


    
}

