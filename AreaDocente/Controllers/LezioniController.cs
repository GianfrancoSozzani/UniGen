using Microsoft.AspNetCore.Mvc;
using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class LezioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LezioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Add()
        {
            return View();
        }

        //LISTA
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var lezione = await dbContext.lezioni.ToListAsync();
            foreach (var lez in lezione)
            {
                lez.Titolo = Guid.NewGuid().ToString();
                lez.Video = Guid.NewGuid().ToString();
            }
            return View(lezione);
        }

    }
}
