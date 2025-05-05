using Microsoft.AspNetCore.Mvc;
using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using LibreriaClassi;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var lez = await dbContext.lezioni
                .Include(a => a.Esame)
                .Where(a => a.Esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToListAsync();

            return View(lez);
        }

        //ADD
        [HttpGet]
        public IActionResult Add()
        {
            PopoloDDL();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLezioniViewModel viewModel)
        {
            PopoloDDL();
            //CONTROLLI FORMALI
            if (viewModel.Titolo == null)
            {
                TempData["ErrorMessage"] = "Titolo mancante!";
                return View(viewModel);
            }
            if (viewModel.Video == null)
            {
                TempData["ErrorMessage"] = "Video mancante!";
                return View(viewModel);
            }
            if (Regex.IsMatch(viewModel.Titolo, @"[^a-zA-Z0-9\s]"))
            {
                TempData["ErrorMessage"] = "Non sono ammessi caratteri speciali nel titolo!";
                return View(viewModel);
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare l'esame!";
                return View(viewModel);
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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var lez = await dbContext.lezioni.FindAsync(id);
            PopoloDDL();
            return View(lez);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MVCLezioni viewModel)
        {
            //CONTROLLI FORMALI
            if (viewModel.Titolo == null)
            {
                TempData["ErrorMessage"] = "Inserire un titolo!";
                return View(viewModel);
            }
            if (viewModel.Video == null)
            {
                TempData["ErrorMessage"] = "Inserire un video!";
                return View(viewModel);
            }
            if (Regex.IsMatch(viewModel.Titolo, @"[^a-zA-Z0-9\s]"))
            {
                TempData["ErrorMessage"] = "Non sono ammessi caratteri speciali nel titolo!";
                return View(viewModel);
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare l'esame!";
                return View(viewModel);
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
        public async Task<IActionResult> Annulla()
        {
            return RedirectToAction("List", "Lezioni");
        }
    }
}
