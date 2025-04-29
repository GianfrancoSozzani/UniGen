using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class FAQController : Controller
    public class FaqController : Controller
    {
        public IActionResult Index()
        public IActionResult Faq()
        {
            return View();
            return View("Faq", "Faq");
        }
    }
}
