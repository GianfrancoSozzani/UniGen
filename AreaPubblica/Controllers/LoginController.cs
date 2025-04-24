using AreaPubblica.Data;
using AreaPubblica.Models;
using AreaPubblica.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaPubblica.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var esiste = await dbContext.Studenti
               .AnyAsync(s => s.Email == viewModel.username && s.Password == viewModel.PWD);

                if (esiste)
                {
                    // Autenticazione riuscita, reindirizza alla pagina principale
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenziali non valide.");
                }
            }
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
