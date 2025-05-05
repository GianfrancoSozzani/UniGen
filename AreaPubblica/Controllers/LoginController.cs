using AreaPubblica.Data;
using AreaPubblica.Models;
using AreaPubblica.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions; // Importante per la sessione

namespace AreaPubblica.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            // Controllo login Studente
            var studente = await dbContext.Studenti
                .Where(s => s.Email == viewModel.username && s.PWD == viewModel.PWD)
                .Select(s => new { s.K_Studente, s.Email, s.Matricola }) // Carico solo K_Studente ed Email e Matricola
                .FirstOrDefaultAsync();

            if (studente != null)
            {
                // Salvo solo ciò che serve
                //HttpContext.Session.SetString("K_Studente", studente.K_Studente.ToString());
                //HttpContext.Session.SetString("Email", studente.Email);
                //HttpContext.Session.SetString("Ruolo", "S");

                if (studente.Matricola == null)
                {
                    //return RedirectToAction("AREA STUDENTE (NON IMMATRICOLATO)", "Home");
                    return Redirect("http://localhost:5201/Studenti/ModificaProfilo?cod=" + studente.K_Studente.ToString() + "&&usr=" + studente.Email + "&&r=s");

                }

                //return RedirectToAction("AREA LAVORO STUDENTE (IMMATRICOLATO)", "Home");
                return Redirect("http://localhost:5201/Studenti/ModificaProfilo?cod=" + studente.K_Studente.ToString() + "&&usr=" + studente.Email + "&&r=s");

            }

            // Controllo login Docente
            var docente = await dbContext.Docenti
                .Where(s => s.Email == viewModel.username && s.PWD == viewModel.PWD)
                .Select(s => new { s.K_Docente, s.Email, s.Abilitato }) // Carico solo K_Docente ed Email e Abilitato
                .FirstOrDefaultAsync();

            if (docente != null)
            {
                //HttpContext.Session.SetString("K_Docente", docente.K_Docente.ToString());
                //HttpContext.Session.SetString("Email", docente.Email);
                //HttpContext.Session.SetString("Ruolo", "D");
                if (docente.Abilitato == "N")
                {
                    return Redirect("http://localhost:5201/Studenti/ModificaProfilo?cod=" + docente.K_Docente.ToString() + "&&usr=" + docente.Email + "&&r=dn");
                    //return RedirectToAction("AREA DOCENTE (NON ABILITATO)", "Home");
                }
                return Redirect("http://localhost:5201/Studenti/ModificaProfilo?cod=" + docente.K_Docente.ToString() + "&&usr=" + docente.Email + "&&r=da");
                //return RedirectToAction("AREA DOCENTE (ABILITATO)", "Home");
            }

            var operatore = await dbContext.Operatori
            .Where(o => o.USR == viewModel.username && o.PWD == viewModel.PWD)
            .Select(o => new { o.K_Operatore, o.USR }) // Carico solo K_Studente ed Email e Matricola
            .FirstOrDefaultAsync();

            if (operatore != null)
            {
                // Salvo solo ciò che serve
                //HttpContext.Session.SetString("K_Operatore", operatore.K_Operatore.ToString());
                //HttpContext.Session.SetString("USR", operatore.USR);
                //HttpContext.Session.SetString("Ruolo", "O");
                return Redirect("http://localhost:5201/Studenti/ModificaProfilo?cod=" + operatore.K_Operatore.ToString() + "&&usr=" + operatore.USR + "&&r=o");
                //return RedirectToAction("AREA AMMINISTRAZIONE", "Home");
            }

            // Nessun utente trovato
            //ModelState.AddModelError("", "Credenziali non valide.");
            TempData["LoginMessage"] = "Credenziali non valide.";
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerificaImmagine(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "Nessun file selezionato." });

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return Json(new { success = false, message = "Formato non valido. Usa JPG, JPEG o PNG." });

            if (file.Length > 10 * 1024 * 1024)
                return Json(new { success = false, message = "Il file supera i 10 MB." });

            return Json(new { success = true, message = "Immagine valida!" });
        }




        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Elaborazione immagine profilo
            if (viewModel.ImmagineFile != null)
            {
                var estensione = Path.GetExtension(viewModel.ImmagineFile.FileName).ToLowerInvariant();
                var estensioniConsentite = new[] { ".jpg", ".jpeg", ".png" };

                if (!estensioniConsentite.Contains(estensione))
                {
                    ModelState.AddModelError("ImmagineFile", "Formato immagine non valido. Sono consentiti solo JPG, JPEG e PNG.");
                    TempData["ErroreUpload"] = "Il file caricato non è un'immagine valida.";
                    return View(viewModel);
                }

                using var ms = new MemoryStream();
                await viewModel.ImmagineFile.CopyToAsync(ms);
                viewModel.ImmagineProfilo = ms.ToArray();
                viewModel.Tipo = estensione;

                TempData["SuccessoUpload"] = "Immagine caricata correttamente.";
            }


            //var corso = await dbContext.Corsi.FirstOrDefaultAsync(); // solo per k_corso
            //if (corso == null)
            //{
            //    ModelState.AddModelError("", "Nessun corso disponibile.");
            //    return View(viewModel);
            //}

            var studente = new Studente
            {
                Nome = viewModel.Nome?.Trim(),
                Cognome = viewModel.Cognome?.Trim(),
                Email = viewModel.Email?.Trim(),
                PWD = viewModel.Password,
                DataNascita = viewModel.DataNascita ?? DateTime.Now,
                Indirizzo = viewModel.Indirizzo,
                CAP = viewModel.CAP,
                Citta = viewModel.Citta,
                Provincia = viewModel.Provincia,
                ImmagineProfilo = viewModel.ImmagineProfilo,
                Tipo = viewModel.Tipo,
                Abilitato = "N",
                DataImmatricolazione = null,
                //K_Corso =  null
            };

            await dbContext.Studenti.AddAsync(studente);
            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registrazione avvenuta con successo!";

            return RedirectToAction("Index", "Home"); 
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("Formato email non valido.");

            var studente = await dbContext.Studenti.FirstOrDefaultAsync(s => s.Email == email);
            if (studente != null)
                return Content($"La tua password è: {studente.PWD}");

            var docente = await dbContext.Docenti.FirstOrDefaultAsync(d => d.Email == email);
            if (docente != null)
                return Content($"La tua password è: {docente.PWD}");

            var operatore = await dbContext.Operatori.FirstOrDefaultAsync(o => o.USR == email);
            if (operatore != null)
                return Content($"La tua password è: {operatore.PWD}");

            return Content("Se l'email è registrata nei nostri sistemi, riceverai una mail con la password.");
        }



    }
}
