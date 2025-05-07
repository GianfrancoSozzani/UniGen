using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class LezioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LezioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //LISTA
        [HttpGet]
        public async Task<IActionResult> List()
        {
            PopoloDDL();

            var lez = await dbContext.lezioni
                .Include(a => a.Esame)
                .Where(a => a.Esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToListAsync();

            return View(lez);
        }

        //ADD
        [HttpPost]
        public async Task<IActionResult> Add(AddLezioniViewModel viewModel)
        {
            //CONTROLLI FORMALI
            if (viewModel.Titolo == null)
            {
                TempData["ErrorMessage"] = "Titolo mancante!";
                return RedirectToAction("List");
            }
            if (viewModel.Video == null)
            {
                TempData["ErrorMessage"] = "Video mancante!";
                return RedirectToAction("List");
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                return RedirectToAction("List");
            }

            var lez = new MVCLezioni
            {
                Titolo = viewModel.Titolo.ToString(),
                Video = viewModel.Video.ToString(),
                K_Esame = (Guid)viewModel.K_Esame
            };
            await dbContext.lezioni.AddAsync(lez);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
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

        //EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(MVCLezioni viewModel)
        {
            //CONTROLLI FORMALI
            if (viewModel.Titolo == null)
            {
                TempData["ErrorMessage"] = "Inserire un titolo!";
                return RedirectToAction("List");
            }
            if (viewModel.Video == null)
            {
                TempData["ErrorMessage"] = "Inserire un video!";
                return RedirectToAction("List");
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                return RedirectToAction("List");
            }

            var lez = await dbContext.lezioni.FindAsync(viewModel.K_Lezione);
            if (lez is not null)
            {
                lez.Titolo = viewModel.Titolo;
                lez.Video = viewModel.Video;
                lez.K_Esame = viewModel.K_Esame;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        //DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(MVCLezioni viewModel)
        {
            var lez = await dbContext.lezioni
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.K_Lezione == viewModel.K_Lezione);
            if (lez is not null)
            {
                dbContext.lezioni.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
    }
}
