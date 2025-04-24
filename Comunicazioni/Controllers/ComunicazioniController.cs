using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comunicazioni.Controllers
{
    public class ComunicazioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ComunicazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //----------------------------------------------//
        //LIST------------------------------------------//
        //----------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var comunicazioni = await dbContext.Comunicazioni.ToListAsync();
            return View(comunicazioni);
        }

        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]  //ricevo i dati dalla lista ed inserisco nel db
        public async Task<IActionResult>Add(AddComunicazioneViewModel viewModel)
        {
            var comunicazione = new Comunicazione
            {
                DataOraComunicazione = viewModel.DataOraComunicazione,
                Soggetto = viewModel.Soggetto,
                Testo = viewModel.Testo,
                K_Studente = viewModel.K_Studente,
                K_Docente = viewModel.K_Docente,
            };

            //collegamento tra studenti e facoltà
            foreach (var riga in comunicazione)
            {
                // var (=> quello che io sto cercando) sarà uguale
                riga.Studente = dbContext.Facolta.FirstOrDefault(u => u.K_Studente == riga.K_Studente); //una join
                riga.Docente = dbContext.Facolta.FirstOrDefault(u => u.K_Docente == riga.K_Docente); //una join

            }

            return View(comunicazione);
            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Listcshtml", "Comunicazioni");
        }
   
        }

    }
}
