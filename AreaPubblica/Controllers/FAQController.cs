using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Faq()
        {
            return View("Faq", "Faq");
        }
    }
}
