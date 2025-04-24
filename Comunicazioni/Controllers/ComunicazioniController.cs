using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //D D L, da controllare in base a view => comunicazioni => add
        public void PopolaDocente()
        {//crea una lista
            IEnumerable<SelectListItem> ListaDocente = dbContext.Docenti.Select(i => new SelectListItem
            {
                Text = i.Nome + " " + i.Cognome + " - " + i.Email, //nel testo descrizione
                Value = i.K_Docente.ToString() //nel value l'id
            });
            ViewBag.DocentiList = ListaDocente; //passo alla lista attraverso ViewBag
        }

        public void PopolaStudente()
        {//crea una lista
            IEnumerable<SelectListItem> ListaStudente = dbContext.Studenti.Select(i => new SelectListItem
            {
                Text = i.Nome + " " + i.Cognome + " - " + i.Email, //nel testo descrizione
                Value = i.K_Studente.ToString() //nel value l'id
            });
            ViewBag.StudentiList = ListaStudente; //passo alla lista attraverso ViewBag
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
