using AreaDocente.Data;
using AreaDocente.Models.Entities;
using AreaDocente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
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
            var appelli = await dbContext.appelli.ToListAsync();
            foreach (var riga in appelli)
            {
                riga.Esame = dbContext.esami.FirstOrDefault(u => u.K_Esame == riga.K_Esame);
            }
            return View(appelli);
        }

        //POPOLO DDL ESAMI
        public void PopoloDDL()
        {
            IEnumerable<SelectListItem> ListaEsami = dbContext.esami.Select(e => new SelectListItem
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
                return View(viewModel);
            }
            if (viewModel.Tipo == null)
            {
                TempData["ErrorMessage"] = "Tipo appello mancante!";
                return View(viewModel);
            }
            if (viewModel.Link == null)
            {
                TempData["ErrorMessage"] = "Link appello mancante!";
                return View(viewModel);
            }
            if (viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare l'esame!";
                return View(viewModel);
            }

            var appello = new MVCAPPELLO
            {
                DataAppello = (DateTime)viewModel.DataAppello,
                DataVerbalizzazione = (DateTime)viewModel.DataVerbalizzazione,
                Tipo = (char)viewModel.Tipo,
                Link = viewModel.Link,
                DataOrale = (DateTime)viewModel.DataOrale,
                K_Esame = (Guid)viewModel.K_Esame
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
            if (viewModel.K_Esame == Guid.Empty)
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
            return RedirectToAction("List", "Lezioni");
        }
    }
}
