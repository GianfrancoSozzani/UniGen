using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class ProveController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProveController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult SelezionaEsame(AddProveViewModel model)
        {
            PopolaEsami();

            // Popola appelli in base all'esame selezionato
            ViewBag.AppelliList = dbContext.appelli
                .Where(a => a.K_Esame == model.K_Esame)
                .Select(a => new SelectListItem
                {
                    Value = a.K_Appello.ToString(),
                    Text = $"{a.DataAppello:dd/MM/yyyy} - {(a.Tipo == "Or" ? "Orale" : a.Tipo == "Sc" ? "Scritto" : a.Tipo == "La" ? "Laurea" : a.Tipo)}"
                })
                .ToList();


            return View("Add", model); // restituisci la stessa view "Add"
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
        public IActionResult Add()
        {
            PopolaEsami();
            ViewBag.AppelliList = new List<SelectListItem>();
            return View(new AddProveViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddProveViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

            }

            var prova = new MVCPROVA
            {
                K_Prova = viewModel.K_Prova != Guid.Empty ? viewModel.K_Prova : Guid.NewGuid(),
                K_Appello = viewModel.K_Appello,
                Tipologia = viewModel.Tipologia,
                Link = viewModel.Link
            };

            await dbContext.prove.AddAsync(prova);

            if (viewModel.Tipologia != "Or")
            {
                // Salva ogni domanda legata a questa prova
                if (viewModel.Domande != null && viewModel.Domande.Any())
                {
                    if (viewModel.Tipologia == "Da")
                    {
                        foreach (var d in viewModel.Domande)
                        {
                            var domanda = new MVCTest_DA
                            {
                                K_Test_DA = Guid.NewGuid(),
                                Numero_Domanda = d.Numero_Domanda,
                                Domanda = d.Domanda,
                                K_Prova = prova.K_Prova
                            };

                            await dbContext.test_DA.AddAsync(domanda);
                        }
                    }
                    else
                    {
                        foreach (var d in viewModel.Domande)
                        {
                            var domanda = new MVCTest_DC
                            {
                                K_Test_DC = Guid.NewGuid(),
                                Numero_Domanda = d.Numero_Domanda,
                                Domanda = d.Domanda,
                                Risposte = d.Risposte,
                                RispostaCorretta = d.RispostaCorretta,
                                K_Prova = prova.K_Prova
                            };

                            await dbContext.test_DC.AddAsync(domanda);
                        }
                    }
                }
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        //LISTA
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var prove = await dbContext.prove
                .Include(a => a.Appello)
                .Include(a => a.Appello.Esame)
                .Where(a => a.Appello.Esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToListAsync();

            foreach (var prova in prove)
            {
                if (prova.Tipologia == "Da")
                {
                    var domandeAperte = await dbContext.test_DA
                        .Where(d => d.K_Prova == prova.K_Prova)
                        .ToListAsync();
                    prova.DomandeAperte = domandeAperte;
                }
                else if (prova.Tipologia == "Dc")
                {
                    var domandeChiuse = await dbContext.test_DC
                        .Where(d => d.K_Prova == prova.K_Prova)
                        .ToListAsync();
                    prova.DomandeChiuse = domandeChiuse;
                }
            }

            return View(prove);
        }
    }
}
