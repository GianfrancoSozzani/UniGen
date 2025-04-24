using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using LibreriaClassi;
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
            //if (amministrazione)
            //{

            //    var comunicazioni = await dbContext.Comunicazioni.ToListAsync();
            //    return View(comunicazioni);
            //}

            //if (docente)
            //{
            //    var comunicazioni = await dbContext.Comunicazioni.FindAsync(esame);
            //    await dbContext.Comunicazioni.ToListAsync();
            //    return View(comunicazioni);
            //}
            //else
            //{
            //    var comunicazioni = await dbContext.Comunicazioni.FindAsync(corso);
            //    await dbContext.Comunicazioni.ToListAsync();
            //    return View(comunicazioni);
            //}
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

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Listcshtml", "Comunicazioni");
        }
   
        



    }
}
