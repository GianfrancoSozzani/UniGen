using AreaDocente.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AreaDocente.Controllers
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
            string? cod = HttpContext.Request.Query["cod"];
            string? usr = HttpContext.Request.Query["usr"];
            string? r = HttpContext.Request.Query["r"];

            if (cod != null && usr != null && r != null)
            {
                HttpContext.Session.SetString("cod", cod);
                HttpContext.Session.SetString("usr", usr);
                HttpContext.Session.SetString("r", r);
            }

            //return View();
            var model = new ListAndAddViewModel
            {
                Comunicazioni = new List<IGrouping<Guid, AreaDocente.Models.Entities.Comunicazione>>(),
                AddComunicazione = new AddComunicazioneViewModel()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contatti()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult ComeFunziona()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("https://localhost:7272");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
