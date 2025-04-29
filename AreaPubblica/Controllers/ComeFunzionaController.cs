using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class ComeFunzionaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
