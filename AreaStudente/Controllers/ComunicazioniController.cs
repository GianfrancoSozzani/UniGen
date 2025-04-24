using Microsoft.AspNetCore.Mvc;

namespace AreaStudente.Controllers
{
    public class ComunicazioniController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
