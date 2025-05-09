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

        // Add Prove
        [HttpPost]
        public ActionResult SelezionaEsameEdit(MVCPROVA model)
        {
            PopolaEsami();

            // Popola appelli in base all'esame selezionato
            ViewBag.AppelliList = dbContext.appelli
                .Where(a => a.K_Esame == model.Appello.K_Esame)
                .Select(a => new SelectListItem
                {
                    Value = a.K_Appello.ToString(),
                    Text = $"{a.DataAppello:dd/MM/yyyy} - {(a.Tipo == "Or" ? "Orale" : a.Tipo == "Sc" ? "Scritto" : a.Tipo == "La" ? "Laurea" : a.Tipo)}",
                    Selected = (a.K_Appello == model.K_Appello)
                })
                .ToList();

            return View("Edit", model); // restituisci la stessa view "Add"
        }

        [HttpPost]
        public async Task<IActionResult> SelezionaStudenti(Guid? K_Prova)
        {
            //ViewBag.ProvaSelezionata = K_Prova;

            PopolaProve(K_Prova);

            if (K_Prova == null)
                return View(new List<MVCValutazione>());

            var studenti = await dbContext.valutazioni
                .Where(v => v.K_Prova == K_Prova)
                .Include(v => v.Studente)
                .Include(v => v.Prova)
                .Where(v => dbContext.libretti.Any(l =>
                    l.K_Studente == v.K_Studente &&
                    l.K_Appello == v.Prova.K_Appello &&
                    l.VotoEsame == null))
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
                    Text = p.Appello?.Esame?.TitoloEsame + " - " + p.Appello?.DataAppello?.ToString("dd/MM/yyyy") + " - " + (p.Tipologia == "Or" ? "Orale" : p.Tipologia == "Da" ? "Domande Aperte" : p.Tipologia == "Dc" ? "Domande Chiuse" : p.Tipologia)
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
                    Text = p.Appello?.Esame?.TitoloEsame + " - " + p.Appello?.DataAppello?.ToString("dd/MM/yyyy") + " - " + (p.Tipologia == "Or" ? "Orale" : p.Tipologia == "Da" ? "Domande Aperte" : p.Tipologia == "Dc" ? "Domande Chiuse" : p.Tipologia),
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

        public void PopolaEsami(Guid? K_Esame)
        {
            IEnumerable<SelectListItem> ListaEsami = dbContext.esami
                .Where(e => e.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .Select(e => new SelectListItem
                {
                    Text = e.TitoloEsame,
                    Value = e.K_Esame.ToString(),
                    Selected = (e.K_Esame == K_Esame)
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
                        .OrderBy(d => d.Numero_Domanda)
                        .ToListAsync();
                    prova.DomandeAperte = domandeAperte;
                }
                else if (prova.Tipologia == "Dc")
                {
                    var domandeChiuse = await dbContext.test_DC
                        .Where(d => d.K_Prova == prova.K_Prova)
                        .OrderBy(d => d.Numero_Domanda)
                        .ToListAsync();
                    prova.DomandeChiuse = domandeChiuse;
                }
            }

            return View(prove);
        }

        //EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? K_Prova)
        {
            var prova = await dbContext.prove
                .Include(p => p.Appello)
                .Include(p => p.Appello.Esame)
                .FirstOrDefaultAsync(p => p.K_Prova == K_Prova);

            if (prova.Tipologia == "Da")
            {
                var domandeAperte = await dbContext.test_DA
                    .Where(d => d.K_Prova == prova.K_Prova)
                    .OrderBy(d => d.Numero_Domanda)
                    .ToListAsync();

                prova.DomandeAperte = domandeAperte;
            }
            else if (prova.Tipologia == "Dc")
            {
                var domandeChiuse = await dbContext.test_DC
                    .Where(d => d.K_Prova == prova.K_Prova)
                    .OrderBy(d => d.Numero_Domanda)
                    .ToListAsync();

                prova.DomandeChiuse = domandeChiuse;
            }

            PopolaEsami();
            return SelezionaEsameEdit(prova);

            //return View(prova);
        }

        //EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(MVCPROVA viewModel)
        {
            //CONTROLLI FORMALI
            if (viewModel.Tipologia == null)
            {
                TempData["ErrorMessage"] = "Inserire la tipologia della prova!";
                return View(viewModel);
            }
            if (viewModel.Link == null)
            {
                TempData["ErrorMessage"] = "Inserire il link di un appello!";
                return View(viewModel);
            }
            if (!viewModel.Appello.K_Esame.HasValue || viewModel.Appello.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                return View(viewModel);
            }
            if (!viewModel.K_Appello.HasValue || viewModel.K_Appello == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un appello!";
                return View(viewModel);
            }

            var prova = await dbContext.prove.FindAsync(viewModel.K_Prova);
            if (prova is not null)
            {
                if (viewModel.Tipologia != prova.Tipologia)
                {
                    if (prova.Tipologia == "Da")
                    {
                        var recordsDADaEliminare = dbContext.test_DA
                            .Where(d => d.K_Prova == prova.K_Prova);
                        dbContext.test_DA.RemoveRange(recordsDADaEliminare);
                    }
                    else if (prova.Tipologia == "Dc")
                    {
                        var recordsDCDaEliminare = dbContext.test_DC
                            .Where(d => d.K_Prova == prova.K_Prova);
                        dbContext.test_DC.RemoveRange(recordsDCDaEliminare);
                    }
                    await dbContext.SaveChangesAsync();
                }

                prova.K_Appello = viewModel.K_Appello;
                prova.Tipologia = viewModel.Tipologia;
                prova.Link = viewModel.Link;

                if (prova.Tipologia == "Da")
                {
                    // 1. Recupera tutte le domande esistenti per questa prova
                    var domandeEsistenti = await dbContext.test_DA
                        .Where(d => d.K_Prova == prova.K_Prova)
                        .ToListAsync();

                    // 2. Crea una lista dei GUID delle domande inviate dal form
                    var domandeInviateIds = viewModel.DomandeAperte?
                        .Select(d => d.K_Test_DA)
                        .ToList() ?? new List<Guid>();

                    // 3. Trova le domande da eliminare (presenti nel DB ma non nel form)
                    var domandeDaEliminare = domandeEsistenti
                        .Where(de => !domandeInviateIds.Contains(de.K_Test_DA))
                        .ToList();

                    // 4. Elimina le domande non più presenti
                    if (domandeDaEliminare.Any())
                    {
                        dbContext.test_DA.RemoveRange(domandeDaEliminare);
                    }

                    if (viewModel.DomandeAperte is not null)
                    {
                        foreach (var d in viewModel.DomandeAperte)
                        {
                            var domanda = await dbContext.test_DA.FindAsync(d.K_Test_DA);
                            if (domanda is not null)
                            {
                                domanda.Numero_Domanda = d.Numero_Domanda;
                                domanda.Domanda = d.Domanda;
                                await dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                var newDomanda = new MVCTest_DA
                                {
                                    K_Test_DA = Guid.NewGuid(),
                                    Numero_Domanda = d.Numero_Domanda,
                                    Domanda = d.Domanda,
                                    K_Prova = prova.K_Prova
                                };
                                await dbContext.test_DA.AddAsync(newDomanda);
                            }
                        }
                    }
                }
                else if (prova.Tipologia == "Dc")
                {
                    // 1. Recupera tutte le domande esistenti per questa prova
                    var domandeEsistenti = await dbContext.test_DC
                        .Where(d => d.K_Prova == prova.K_Prova)
                        .ToListAsync();

                    // 2. Crea una lista dei GUID delle domande inviate dal form
                    var domandeInviateIds = viewModel.DomandeChiuse?
                        .Select(d => d.K_Test_DC)
                        .ToList() ?? new List<Guid>();

                    // 3. Trova le domande da eliminare (presenti nel DB ma non nel form)
                    var domandeDaEliminare = domandeEsistenti
                        .Where(de => !domandeInviateIds.Contains(de.K_Test_DC))
                        .ToList();

                    // 4. Elimina le domande non più presenti
                    if (domandeDaEliminare.Any())
                    {
                        dbContext.test_DC.RemoveRange(domandeDaEliminare);
                    }

                    if (viewModel.DomandeAperte is not null)
                    {
                        foreach (var d in viewModel.DomandeChiuse)
                        {
                            var domanda = await dbContext.test_DC.FindAsync(d.K_Test_DC);
                            if (domanda is not null)
                            {
                                domanda.Numero_Domanda = d.Numero_Domanda;
                                domanda.Domanda = d.Domanda;
                                domanda.Risposte = d.Risposte;
                                await dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                var newDomanda = new MVCTest_DC
                                {
                                    K_Test_DC = Guid.NewGuid(),
                                    Numero_Domanda = d.Numero_Domanda,
                                    Domanda = d.Domanda,
                                    Risposte = d.Risposte,
                                    K_Prova = prova.K_Prova
                                };
                                await dbContext.test_DC.AddAsync(newDomanda);
                            }
                        }
                    }
                }
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        //DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(MVCPROVA viewModel)
        {
            dbContext.Entry(viewModel.Appello).State = EntityState.Unchanged;

            var prova = await dbContext.prove
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.K_Prova == viewModel.K_Prova);
            if (prova is not null)
            {
                if (prova.Tipologia == "Da")
                {
                    var recordsDADaEliminare = dbContext.test_DA
                        .Where(d => d.K_Prova == prova.K_Prova);
                    dbContext.test_DA.RemoveRange(recordsDADaEliminare);
                }
                else
                {
                    if (prova.Tipologia == "Dc")
                    {
                        var recordsDCDaEliminare = dbContext.test_DC
                            .Where(d => d.K_Prova == prova.K_Prova);
                        dbContext.test_DC.RemoveRange(recordsDCDaEliminare);
                    }
                }
                dbContext.prove.Remove(viewModel);

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
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
            var appello = await dbContext.appelli.FirstOrDefaultAsync(a => a.K_Appello == K_Appello);

            if (libretto is not null && appello is not null)
            {
                appello.DataVerbalizzazione = DateTime.Now;
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
