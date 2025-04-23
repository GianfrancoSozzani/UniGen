using Microsoft.AspNetCore.Mvc;

namespace AreaPubblica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
