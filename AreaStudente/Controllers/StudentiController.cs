using AreaStudente.Data;
using AreaStudente.Models; // Assicurati che i namespace siano corretti
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessario per metodi come FirstOrDefaultAsync
using System.Threading.Tasks;

namespace AreaStudente.Controllers
{
    public class StudentiController : Controller
    {

        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View();
        }
    }
}
