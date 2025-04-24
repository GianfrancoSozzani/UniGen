using Microsoft.AspNetCore.Mvc;

namespace AreaStudente.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
