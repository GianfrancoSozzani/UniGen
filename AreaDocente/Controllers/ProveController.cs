using Microsoft.AspNetCore.Mvc;

namespace AreaDocente.Controllers
{
    public class ProveController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View();
        }
    }
}
