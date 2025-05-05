using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
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

        public IActionResult PopolaAppelli(Guid K_Esame)
        {
            //var dati = (
            //    from Prova in dbContext.prove
            //    join Appello in dbContext.appelli on Prova.K_Appello equals Appello.K_Appello
            //    join Esami in dbContext.esami on Appello.K_Esame equals Esami.K_Esame
            //    join Docenti in dbContext.docenti on Esami.K_Docente equals Docenti.K_Docente
            //    select new
            //    {
            //        id = Prova.K_Appello,
            //        data_A = Appello.DataAppello,
            //        tipo = Prova.Tipologia,
            //        link = Prova.Link,
            //        data_O = Appello.DataOrale,
            //        esame = Esami.TitoloEsame,

            //    }).ToList<dynamic>();

            IEnumerable<SelectListItem> ListaAppelli = dbContext.appelli
                .Where(e => e.K_Esame == K_Esame)
                .Select(e => new SelectListItem
                {
                    Text = $"{e.DataAppello.ToString()} - {e.Tipo.ToString()}",
                    Value = e.K_Appello.ToString()
                });

            ViewBag.AppelliList = ListaAppelli;
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
            ViewBag.EsamiList = ListaEsami;
        }

        [HttpGet]
        public ActionResult Add()
        {
            PopolaEsami();
            //PopolaAppelli();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddProveViewModel viewModel)
        {
            MVCPROVA prova = new MVCPROVA
            {
                K_Appello = viewModel.K_Appello,
                Link = viewModel.Link,
                Tipologia = viewModel.Tipologia
            };

            await dbContext.prove.AddAsync(prova);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View();
        }
    }
}
