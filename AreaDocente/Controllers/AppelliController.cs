using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class AppelliController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AppelliController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //LISTA
        [HttpGet]
        public async Task<IActionResult> List()
        {
            //var appelli = await dbContext.appelli.ToListAsync();
            //foreach (var riga in appelli)
            //{
            //    riga.Esame = dbContext.esami.FirstOrDefault(u => u.K_Esame == riga.K_Esame);
            //}

            var appelli = await dbContext.appelli
                .Include(a => a.Esame)
                .Where(a => a.Esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToListAsync();

            return View(appelli);
        }

        //POPOLO DDL ESAMI
        public void PopoloDDL()
        {
            IEnumerable<SelectListItem> ListaEsami = dbContext.esami
                .Where(e => e.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .Select(e => new SelectListItem
                {
                    Text = e.TitoloEsame,
                    Value = e.K_Esame.ToString()
                });
            ViewBag.EsamiDDL = ListaEsami;
        }

        //ADD
        [HttpGet]
        public IActionResult Add()
        {
            PopoloDDL();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAppelliViewModel viewModel)
        {
            PopoloDDL();
            //CONTROLLI FORMALI
            if (viewModel.DataAppello == null)
            {
                TempData["ErrorMessage"] = "Data appello mancante!";
                PopoloDDL();
                return View(viewModel);
            }
            if (viewModel.Tipo == null)
            {
                TempData["ErrorMessage"] = "Tipo appello mancante!";
                PopoloDDL();
                return View(viewModel);
            }
            if (viewModel.Link == null)
            {
                TempData["ErrorMessage"] = "Link appello mancante!";
                PopoloDDL();
                return View(viewModel);
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                PopoloDDL();
                return View(viewModel);
            }

            var appello = new MVCAPPELLO
            {
                DataAppello = viewModel.DataAppello,
                DataVerbalizzazione = viewModel.DataVerbalizzazione,
                Tipo = viewModel.Tipo,
                Link = viewModel.Link,
                K_Esame = viewModel.K_Esame
            };
            await dbContext.appelli.AddAsync(appello);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        //EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var appello = await dbContext.appelli.FindAsync(id);
            PopoloDDL();
            return View(appello);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MVCAPPELLO viewModel)
        {
            //CONTROLLI FORMALI
            if (viewModel.DataAppello == null)
            {
                TempData["ErrorMessage"] = "Inserire la data di un appello!";
                PopoloDDL();
                return View(viewModel);
            }
            if (viewModel.Tipo == null)
            {
                TempData["ErrorMessage"] = "Inserire il tipo di un appello!";
                PopoloDDL();
                return View(viewModel);
            }
            if (viewModel.Link == null)
            {
                TempData["ErrorMessage"] = "Inserire il link di un appello!";
                PopoloDDL();
                return View(viewModel);
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                PopoloDDL();
                return View(viewModel);
            }
            if (viewModel.DataVerbalizzazione < viewModel.DataAppello)
            {
                TempData["ErrorMessage"] = "La data di verbalizzazione non puo essere precedente alla data dell appello.";
                PopoloDDL();
                return View(viewModel);
            }



            var appello = await dbContext.appelli.FindAsync(viewModel.K_Appello);
            if (appello is not null)
            {
                appello.DataAppello = viewModel.DataAppello;
                appello.Tipo = viewModel.Tipo;

                if (viewModel.DataVerbalizzazione != null)
                    appello.DataVerbalizzazione = viewModel.DataVerbalizzazione;

                appello.Link = viewModel.Link;
                appello.K_Esame = viewModel.K_Esame;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MVCAPPELLO viewModel)
        {
            var appello = await dbContext.appelli
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.K_Appello == viewModel.K_Appello);
            if (appello is not null)
            {
                dbContext.appelli.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Annulla()
        {
            return RedirectToAction("List", "Appelli");
        }
    }
}
