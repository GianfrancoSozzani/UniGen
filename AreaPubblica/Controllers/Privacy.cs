using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class Privacy : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
