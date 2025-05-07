using System.Diagnostics;
using AreaStudente.Data;
using AreaStudente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaStudente.Controllers
{
    public class HomeController : Controller

    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Contatti()
        {
            var studenteIdStr = HttpContext.Session.GetString("cod");

            ViewData["studente_id"] = studenteIdStr;

            if (Guid.TryParse(studenteIdStr, out Guid studenteId))
            {
                ViewBag.StudenteId = studenteId;

                // Query al database
                var studente = _context.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);

                if (studente != null)
                {
                    ViewData["email"] = studente.Email;
                    ViewData["matricola"] = studente.Matricola;
                    ViewData["abilitato"] = studente.Abilitato;
                }
            }
            else
            {
                ViewBag.StudenteId = "ID non trovato nella sessione.";
            }
            return View();
        }
        public IActionResult Come_Funziona() 
        {
            var studenteIdStr = HttpContext.Session.GetString("cod");

            ViewData["studente_id"] = studenteIdStr;

            if (Guid.TryParse(studenteIdStr, out Guid studenteId))
            {
                ViewBag.StudenteId = studenteId;

                // Query al database
                var studente = _context.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);

                if (studente != null)
                {
                    ViewData["email"] = studente.Email;
                    ViewData["matricola"] = studente.Matricola;
                    ViewData["abilitato"] = studente.Abilitato;
                }
            }
            else
            {
                ViewBag.StudenteId = "ID non trovato nella sessione.";
            }
            return View();
        }
        public IActionResult Privacy()
        {
            var studenteIdStr = HttpContext.Session.GetString("cod");

            ViewData["studente_id"] = studenteIdStr;

            if (Guid.TryParse(studenteIdStr, out Guid studenteId))
            {
                ViewBag.StudenteId = studenteId;

                // Query al database
                var studente = _context.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);

                if (studente != null)
                {
                    ViewData["email"] = studente.Email;
                    ViewData["matricola"] = studente.Matricola;
                    ViewData["abilitato"] = studente.Abilitato;
                }
            }
            else
            {
                ViewBag.StudenteId = "ID non trovato nella sessione.";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
