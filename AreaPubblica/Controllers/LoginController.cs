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
                .Where(s => s.Email == viewModel.username && s.PWD == viewModel.PWD)
                .Select(s => new { s.K_Studente, s.Email, s.Matricola }) // Carico solo K_Studente ed Email e Matricola
                .FirstOrDefaultAsync();

            if (studente != null)
            {
                // Salvo solo ciò che serve
                HttpContext.Session.SetString("K_Studente", studente.K_Studente.ToString());
                HttpContext.Session.SetString("Email", studente.Email);
                HttpContext.Session.SetString("Ruolo", "S");

                if (studente.Matricola == null)
                {
                    //return RedirectToAction("AREA STUDENTE (NON IMMATRICOLATO)", "Home");
                    return Redirect("http://localhost:5201/Studenti/ModificaProfilo");

                }

                //return RedirectToAction("AREA LAVORO STUDENTE (IMMATRICOLATO)", "Home");
                return Redirect("http://localhost:5201/Studenti/ModificaProfilo");

            }

            // Controllo login Docente
            var docente = await dbContext.Docenti
                .Where(s => s.Email == viewModel.username && s.PWD == viewModel.PWD)
                .Select(s => new { s.K_Docente, s.Email, s.Abilitato }) // Carico solo K_Docente ed Email e Abilitato
                .FirstOrDefaultAsync();

            if (docente != null)
            {
                HttpContext.Session.SetString("K_Docente", docente.K_Docente.ToString());
                HttpContext.Session.SetString("Email", docente.Email);
                HttpContext.Session.SetString("Ruolo", "D");
                if (docente.Abilitato == "N")
                {
                    //return RedirectToAction("AREA DOCENTE (NON ABILITATO)", "Home");
                }
                //return RedirectToAction("AREA DOCENTE (ABILITATO)", "Home");
            }

            var operatore = await dbContext.Operatori
            .Where(o => o.USR == viewModel.username && o.PWD == viewModel.PWD)
            .Select(o => new { o.K_Operatore, o.USR }) // Carico solo K_Studente ed Email e Matricola
            .FirstOrDefaultAsync();

            if (operatore != null)
            {
                // Salvo solo ciò che serve
                HttpContext.Session.SetString("K_Operatore", operatore.K_Operatore.ToString());
                HttpContext.Session.SetString("USR", operatore.USR);
                HttpContext.Session.SetString("Ruolo", "O");

                //return RedirectToAction("AREA AMMINISTRAZIONE", "Home");
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
                PWD = viewModel.Password,
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
