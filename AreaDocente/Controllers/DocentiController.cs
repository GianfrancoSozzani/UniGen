using AreaDocente.Data;
using AreaDocente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class DocentiController : Controller
    {
        public readonly DbContext dbContext;

        public DocentiController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> ModificaProfilo()
        {
            // Simulo l'ID, solo per test (poi userò la sessione)
            var studenteId = new Guid("0CDDDB26-14FE-4937-91A8-ED9014654CA3");
            var studente = await dbContext.Docenti.FirstOrDefaultAsync(s => s.K_Studente == studenteId);

            if (studente == null)
                return NotFound();

            var model = new ModificaDocenteViewModel
            {
                Email = studente.Email,
                Nome = studente.Nome,
                Cognome = studente.Cognome,
                Indirizzo = studente.Indirizzo,
                CAP = studente.CAP,
                Citta = studente.Citta,
                Provincia = studente.Provincia,
                DataNascita = studente.DataNascita,
                ImmagineProfilo = studente.ImmagineProfilo
            };
            //In questo caso il nome della view viene dedotto dall'action del controller (che ha lo stesso nome).
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModificaProfilo(ModificaDocenteViewModel model, string PasswordNew, string PasswordConfirm)
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
                var tipo = model.ImmagineProfiloFile.ContentType.ToLower();
                if (tipo != "image/jpeg" && tipo != "image/jpg" && tipo != "image/png")
                {
                    TempData["AlertMessage"] = "Formato non valido. Sono accettati solo JPG, JPEG e PNG.";
                    return RedirectToAction("ModificaProfilo");
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
