using AreaPubblica.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaPubblica.Controllers
{
    public class OffertaFormativaController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public OffertaFormativaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        
        public async Task <IActionResult> PopoloCorsiLaurea()
        {
            var corso = await dbContext.Corsi.ToListAsync();
            return Json(corso);
        }
    }
}
