using AreaStudente.Data;
using AreaStudente.Models;
using AreaStudente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AreaStudente.Controllers
{
    public class ComunicazioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ComunicazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //popola la dropdownlist dei docenti
        public void PopolaDocenti()
        {
            IEnumerable<SelectListItem> ListaDocenti = dbContext.Studenti.Select(i => new SelectListItem()
            {
                Text = i.descrizione,
                Value = i.IdFacolta.ToString()
            });
            ViewBag.FacoltaList = ListaFacolta;


        }

        [HttpPost]
        public async Task<IActionResult> Add(AddComunicazioneViewModel viewModel)
        {
            var comunicazione = new Comunicazione
            {
                DataOraComunciazione = viewModel.DataOraComunciazione,
                Soggetto = viewModel.Soggetto,
                Testo = viewModel.Testo,
                Nome = viewModel.Nome,
                Cognome = viewModel.Cognome,
                K_Docente = viewModel.K_Docente,
                K_Studente = viewModel.K_Studente,
                Codice_Comunicazione=viewModel.Codice_Comunicazione,
                K_Soggetto=viewModel.K_Soggetto

            };

            await dbContext.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Studenti");
        }
    }
}
