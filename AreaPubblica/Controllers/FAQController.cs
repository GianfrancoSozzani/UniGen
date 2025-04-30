using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
