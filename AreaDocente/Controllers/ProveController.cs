using AreaDocente.Data;
using AreaDocente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AreaDocente.Controllers
{
    public class ProveController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProveController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Appelli()
        {
            var dati = (
                from Prova in dbContext.prove
                join Appello in dbContext.appelli on Prova.K_Appello equals Appello.K_Appello
                join Esami in dbContext.esami on Appello.K_Esame equals Esami.K_Esame
                join Docenti in dbContext.docenti on Esami.K_Docente equals Docenti.K_Docente
                select new
                {
                    id = Prova.K_Appello,
                    data_A = Appello.DataAppello,
                    tipo = Prova.Tipologia,
                    link = Prova.Link,
                    data_O = Appello.DataOrale,
                    esame = Esami.TitoloEsame,

                }).ToList<dynamic>();

            ViewBag.Appelli = dati;
            return View();
        }
        public IActionResult ValutazioneTest()
        {
            
            return View();
        }
        public void PopolaEsami()
        {
            IEnumerable<SelectListItem> ListaEsami = dbContext.esami
                .Where(e => e.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .Select(e => new SelectListItem
                {
                    Text = e.TitoloEsame,
                    Value = e.K_Esame.ToString()
                });
            ViewBag.EsameList = ListaEsami;
        }
        [HttpGet]
        public ActionResult Add()
        {
            PopolaEsami();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddProveViewModel viewModel) 
        {


            return View();
        }


    }
}
