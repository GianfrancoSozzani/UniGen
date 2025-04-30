using System.Diagnostics;
using AreaDocente.Models;
using Microsoft.AspNetCore.Mvc;

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

            HttpContext.Session.SetString("cod", cod);
            HttpContext.Session.SetString("usr", usr);
            HttpContext.Session.SetString("r", r);

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
