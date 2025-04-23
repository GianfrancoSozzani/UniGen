using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class Contatti : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
