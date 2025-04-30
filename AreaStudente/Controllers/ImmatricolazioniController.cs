using AreaStudente.Data;
using AreaStudente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaStudente.Controllers
{
    public class ImmatricolazioniController : Controller
    {

        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public ImmatricolazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddImmatricolazioneViewModel viewModel)
        {
            var studente = await dbContext.Immatricolazioni.FirstOrDefaultAsync(s => s.K_Studente = viewModel.K_Studente);
            return View(studente);
        }
    }
}
