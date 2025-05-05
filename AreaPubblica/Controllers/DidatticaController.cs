using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class DidatticaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Seduta()
        {
            return View();
        }
    }
}
