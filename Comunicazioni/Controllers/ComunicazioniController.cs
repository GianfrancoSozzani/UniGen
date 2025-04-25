using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using LibreriaClassi;
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
            //Test lista
            
            //var comunicazioni = await dbContext.Comunicazioni.ToListAsync();
            //foreach (var record in comunicazioni)
            //{
            //    record.Studente = dbContext.Studenti.FirstOrDefault(u => u.K_Studente == record.K_Studente);
            //    record.Docente = dbContext.Docenti.FirstOrDefault(u => u.K_Docente == record.K_Docente);
            //}
            var comunicazioni = await dbContext.Comunicazioni
                .Include(c => c.Studente)
                .Include(c => c.Docente)
                .ToListAsync();
            return View(comunicazioni);
        }

        //D D L, da controllare in base a view => comunicazioni => add
        // dropdown da eventualmente collegare(?) RIVEDERE
        public void PopolaEsami()
        {//crea una lista
            IEnumerable<SelectListItem> ListaEsami = dbContext.Esami.Select(i => new SelectListItem
            {
                Text = i.TitoloEsame, //nel testo descrizione
                Value = i.K_Esame.ToString() //nel value l'id
            });
            ViewBag.EsamiList = ListaEsami; //passo alla lista attraverso ViewBag
        }


        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        public void PopolaEsami()
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami.Select(i => new SelectListItem
            {
                Text = i.TitoloEsame,
                Value = i.K_Esame.ToString()
            });
            ViewBag.EsamiList = listaEsami;
        }

        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            PopolaEsami();
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
            };
            
            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Comunicazioni");
        }
    }
}
