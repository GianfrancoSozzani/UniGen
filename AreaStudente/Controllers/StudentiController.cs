using AreaStudente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AreaStudente.Controllers
{
    public class StudentiController : Controller
    {

        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View();
        }
    }
}
