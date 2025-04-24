using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class LezioniController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/[controller]")]
        [ApiController]
        public class LezioniControllers : ControllerBase
        {
            private readonly ApplicationDbContext dbContext;
        }

        public LezioniControllers(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        [Route("Getlist")]
        public async Task<ActionResult<IEnumerable<Lezione>>> Getlist()
        {
            var lezione = await dbContext.Lezione.ToListAsync();
            foreach (var riga in lezione)
            {
                riga.Facolta = dbContext.Facolta.FirstOrDefault(u => u.IdFacolta == riga.IdFacolta);
            }
            return lezione; // rsitituisco un json interrogabile da chiunque
        }
    }
}
