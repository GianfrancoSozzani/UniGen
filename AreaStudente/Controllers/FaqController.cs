using Microsoft.AspNetCore.Mvc;

namespace AreaStudente.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Faq()
        {
            return View();
        }
    }
}

