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

        // Add Prove
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

        [HttpPost]
        public async Task<IActionResult> SelezionaStudenti(Guid? K_Prova)
        {
            //ViewBag.ProvaSelezionata = K_Prova;

            PopolaProve(K_Prova);

            if (K_Prova == null)
                return View(new List<MVCValutazione>());

            //K_Prova = Guid.Parse("4B2E9FD5-23A5-4994-8AE4-00C927AB052B");

            //var studenti = await dbContext.valutazioni
            //    .Where(v => v.K_Prova == K_Prova)
            //    .Select(v => v.Studente)
            //    .ToListAsync();

            //var studenti = await dbContext.valutazioni
            //    .Where(v => v.K_Prova == K_Prova)
            //    .Include(v => v.Studente)
            //    .Include(v => v.Prova)
            //    .ToListAsync();

            var studenti = await dbContext.valutazioni
                .Where(v => v.K_Prova == K_Prova &&
                            dbContext.libretti.Any(l => l.K_Studente == v.K_Studente && l.VotoEsame == null))
                .Include(v => v.Studente)
                .Include(v => v.Prova)
                .ToListAsync();

            return View("Valutazione", studenti);
        }

        public void PopolaProve()
        {
            IEnumerable<SelectListItem> ListaProve = dbContext.prove
                .Include(p => p.Appello)
                .Include(p => p.Appello.Esame)
                .Where(p => p.Appello.Esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToList()
                .Select(p => new SelectListItem
                {
                    Value = p.K_Prova.ToString(),
                    Text = p.Appello?.Esame?.TitoloEsame + " - " + p.Appello?.DataAppello?.ToString("dd/MM/yyyy")
                });
            ViewBag.ProveList = ListaProve;
        }

        public void PopolaProve(Guid? K_Prova)
        {
            IEnumerable<SelectListItem> ListaProve = dbContext.prove
                .Include(p => p.Appello)
                .Include(p => p.Appello.Esame)
                .Where(p => p.Appello.Esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToList()
                .Select(p => new SelectListItem
                {
                    Value = p.K_Prova.ToString(),
                    Text = p.Appello?.Esame?.TitoloEsame + " - " + p.Appello?.DataAppello?.ToString("dd/MM/yyyy"),
                    Selected = (p.K_Prova == K_Prova)
                });
            ViewBag.ProveList = ListaProve;
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

        [HttpGet]
        public IActionResult Valutazione()
        {
            PopolaProve();

            return View(new List<MVCValutazione>());
        }

        [HttpPost]
        public async Task<IActionResult> SalvaValutazione(Guid K_Studente, Guid K_Appello, byte Voto)
        {
            var libretto = await dbContext.libretti.FirstOrDefaultAsync(l => l.K_Studente == K_Studente && l.K_Appello == K_Appello);

            if (libretto != null)
            {
                libretto.VotoEsame = Voto;

                if (Voto >= 18)
                    libretto.Esito = 'S';
                else
                    libretto.Esito = 'N';

                await dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Valutazione salvata con successo.";
            }

            return RedirectToAction("Valutazione");
        }

    }
}
