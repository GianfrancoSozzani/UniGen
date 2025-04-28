using AreaPubblica.Data;
using AreaPubblica.Models;
using AreaPubblica.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Importante per la sessione

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
                return View(viewModel);
            }

            // Controllo login Studente
            var studente = await dbContext.Studenti
                .Where(s => s.Email == viewModel.username && s.Password == viewModel.PWD)
                .Select(s => new { s.K_Studente, s.Email }) // Carico solo K_Studente ed Email
                .FirstOrDefaultAsync();

            if (studente != null)
            {
                // Salvo solo ciò che serve
                HttpContext.Session.SetString("Email", studente.Email ?? "");
                HttpContext.Session.SetString("IdStudente", studente.K_Studente.ToString());


                return RedirectToAction("Index", "Home");
            }

            // Controllo login Docente
            var docente = await dbContext.Docenti
                .FirstOrDefaultAsync(d => d.Email == viewModel.username && d.Password == viewModel.PWD);

            if (docente != null)
            {
                HttpContext.Session.SetString("Email", docente.Email ?? "");
                HttpContext.Session.SetString("IdDocente", docente.K_Docente.ToString());

                // I docenti non hanno Matricola
                HttpContext.Session.Remove("Matricola");

                return RedirectToAction("Index", "Home");
            }

            // Nessun utente trovato
            ModelState.AddModelError("", "Credenziali non valide.");
            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Recupera un corso esistente
            var corso = await dbContext.Corsi.FirstOrDefaultAsync();
            if (corso == null)
            {
                ModelState.AddModelError("", "Nessun corso disponibile.");
                return View(viewModel);
            }

            // Ora puoi creare lo studente associato al corso
            var studente = new Studente
            {
                K_Studente = Guid.NewGuid(),
                Nome = viewModel.Nome?.Trim(),
                Cognome = viewModel.Cognome?.Trim(),
                Email = viewModel.Email?.Trim(),
                Password = viewModel.Password,
                DataNascita = viewModel.DataNascita ?? DateTime.Now,
                Indirizzo = viewModel.Indirizzo,
                CAP = viewModel.CAP,
                Citta = viewModel.Citta,
                Provincia = viewModel.Provincia,
                ImmagineProfilo = viewModel.ImmagineProfilo,
                Tipo = viewModel.Tipo,
                Matricola = viewModel.Matricola,
                Abilitato = "Sì",
                DataImmatricolazione = DateTime.Now,
                K_Corso = corso.K_Corso // 👈 Associazione corretta
            };

            await dbContext.Studenti.AddAsync(studente);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Login", "Login");
        }
    }
}
