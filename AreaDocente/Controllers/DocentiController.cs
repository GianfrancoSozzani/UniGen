using Microsoft.AspNetCore.Mvc;

namespace AreaDocente.Controllers
{
    public class DocentiController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
