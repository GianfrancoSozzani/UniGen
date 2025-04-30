using AreaDocente.Data;
using AreaDocente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class DocentiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public DocentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ModificaProfilo()
        {
            // Simulo l'ID, solo per test (poi userò la sessione)
            var docenteId = new Guid("370CE828-DD9D-46C2-9312-0DCAE28F1B13");
            var docente = await dbContext.docenti.FirstOrDefaultAsync(d => d.K_Docente == docenteId);

            if (docente == null)
                return NotFound();

            var model = new ModificaDocenteViewModel
            {
                Email = docente.Email,
                Nome = docente.Nome,
                Cognome = docente.Cognome,
                Indirizzo = docente.Indirizzo,
                CAP = docente.CAP,
                Citta = docente.Citta,
                Provincia = docente.Provincia,
                DataNascita = docente.DataNascita,
                ImmagineProfilo = docente.ImmagineProfilo
            };
            //In questo caso il nome della view viene dedotto dall'action del controller (che ha lo stesso nome).
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModificaProfilo(ModificaDocenteViewModel model, string PasswordNew, string PasswordConfirm)
        {
            var docenteId = new Guid("370CE828-DD9D-46C2-9312-0DCAE28F1B13");
            var docente = await dbContext.docenti.FirstOrDefaultAsync(d => d.K_Docente == docenteId);

            if (docente == null)
                return NotFound();

            // Aggiorna i dati anagrafici
            docente.Nome = model.Nome;
            docente.Cognome = model.Cognome;
            docente.Indirizzo = model.Indirizzo;
            docente.CAP = model.CAP;
            docente.Citta = model.Citta;
            docente.Provincia = model.Provincia;
            docente.DataNascita = model.DataNascita;

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
                    docente.ImmagineProfilo = memoryStream.ToArray();
                    docente.Tipo = tipo;
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
                if (model.PWD != docente.PWD)
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
                docente.PWD = PasswordNew;
            }


            await dbContext.SaveChangesAsync();
            TempData["DisplaySuccessMsg"] = true;
            TempData["PopupSuccesso"] = "I dati sono stati salvati correttamente.";
            return RedirectToAction("ModificaProfilo");
        }
    }
}
