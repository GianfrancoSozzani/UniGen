using Microsoft.AspNetCore.Mvc;

namespace AreaDocente.Controllers
{
    public class LezioniController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
