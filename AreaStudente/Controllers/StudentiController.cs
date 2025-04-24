using AreaStudente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AreaStudente.Controllers
{
    public class StudentiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ModificaProfilo()
        {
            return View();
        }
    }
}
