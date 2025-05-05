using AreaDocente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AreaDocente.Controllers
{
    public class ProveController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProveController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Appelli()
        {
            var dati = (
                from Appelli in dbContext.appelli
                join Esami in dbContext.esami on Appelli.K_Esame equals Esami.K_Esame
                join Docenti in dbContext.docenti on Esami.K_Docente equals Docenti.K_Docente
                select new
                {
                    data_A = Appelli.DataAppello,
                    data_V = Appelli.DataVerbalizzazione,
                    tipo = Appelli.Tipo,
                    link = Appelli.Link,
                    data_O = Appelli.DataOrale,
                    esame = Esami.TitoloEsame,

                }).ToList<dynamic>();

            ViewBag.Appelli = dati;
            return View();
        }
        public IActionResult ValutazioneTest()
        {

            return View();
        }
    }
}
