using Microsoft.AspNetCore.Mvc;

namespace AreaDocente.Controllers
{
    public class EsamiController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
