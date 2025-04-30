using System.Diagnostics;
using Comunicazioni.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comunicazioni.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            string ruolo = HttpContext.Session.GetString("Ruolo");

            // Se "ruolo" è null o vuoto, l'utente non è autenticato
            if (String.IsNullOrEmpty(ruolo))
            {
                // Reindirizza alla pagina di login
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
