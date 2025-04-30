using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Controllers
{
    public class MaterialiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MaterialiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void PopolaEsame()
        {
            IEnumerable<SelectListItem> ListaEsame = dbContext.esami.Select(i => new SelectListItem
            {
                Text = i.TitoloEsame,
                Value = i.K_Esame.ToString()
            });
            ViewBag.EsameList = ListaEsame;
        }
        [HttpGet]
        public ActionResult Add()
        {
            PopolaEsame();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddMaterialiViewModel viewModel)
        {
            var materiali = new MVCMateriali
            {
                Titolo = viewModel.Titolo.ToString(),
                K_Esame = viewModel.K_Esame,
            };
            if (viewModel.materiale != null && viewModel.materiale.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.materiale.CopyToAsync(memoryStream);

                    materiali.Materiale = memoryStream.ToArray();
                    materiali.Tipo = viewModel.materiale.ContentType;

                }

            }

            await dbContext.materiali.AddAsync(materiali);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Lista", "Materiali");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var materiali = await dbContext.materiali.ToListAsync();
            foreach (var item in materiali)
            {
                item.esame = await dbContext.esami.FirstOrDefaultAsync(e => e.K_Esame == item.K_Esame);
            }
            // var materiali =  dbContext.materiali
            //.Include(m => m.esame)
            //.ToList();

            return View(materiali);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid chiave)
        {
            var materiale = await dbContext.materiali.FindAsync(chiave);
            PopolaEsame();
            return View(materiale);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MVCMateriali viewModel, IFormFile file)
        {
            var materiale = await dbContext.materiali.FindAsync(viewModel.K_Materiale);
            if (materiale is not null) 
            {
                materiale.Titolo = viewModel.Titolo;

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    materiale.Materiale = ms.ToArray();
                }
                materiale.Tipo = viewModel.Tipo;
                materiale.K_Esame = viewModel.K_Esame;
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Lista", "Materiali");
            }
            return View(materiale);
        }
    }
}
