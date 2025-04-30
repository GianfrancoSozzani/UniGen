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

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var materiali = await dbContext.materiali.ToListAsync();
            foreach (var item in materiali)
            {
                item.esame = await dbContext.esami.FirstOrDefaultAsync(e => e.K_Esame == item.K_Esame);
            }


            return View(materiali);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var materiale = await dbContext.materiali.FindAsync(id);
            var mat = new EditMaterialiViewModel
            {
                K_Materiale = materiale.K_Materiale,
                Titolo = materiale.Titolo,
                K_Esame = materiale.K_Esame,
                Materiale = materiale.Materiale,
                Tipo = materiale.Tipo
            };
            PopolaEsame();
            return View(mat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMaterialiViewModel viewModel)
        {
            var materiale = await dbContext.materiali.FindAsync(viewModel.K_Materiale);
            if (materiale is not null)
            {
                materiale.Titolo = viewModel.Titolo;
                if (viewModel.MaterialeDA != null && viewModel.MaterialeDA.Length > 0)
                {
                    viewModel.Tipo = viewModel.MaterialeDA.ContentType;
                    using (var ms = new MemoryStream())
                    {
                        await viewModel.MaterialeDA.CopyToAsync(ms);
                        materiale.Materiale = ms.ToArray();
                        materiale.Tipo = viewModel.Tipo;
                    }
                }
                materiale.K_Esame = viewModel.K_Esame;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MVCMateriali viewModel)
        {
            var mat = await dbContext.materiali
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.K_Materiale == viewModel.K_Materiale);
            if (mat is not null)
            {
                dbContext.materiali.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        private string EstensioneDaContentType(string contentType)
        {
            return contentType switch
            {
                "application/pdf" => ".pdf",
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "application/msword" => ".doc",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
                "application/vnd.ms-excel" => ".xls",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ".xlsx",
                "text/plain" => ".txt",
                "application/zip" => ".zip",
                _ => "" // fallback se il tipo non è riconosciuto
            };
        }

        public async Task<IActionResult> Download(Guid id)
        {
            var materiale = await dbContext.materiali.FindAsync(id);
            if (materiale == null || materiale.Materiale == null)
                return NotFound();

            materiale.Titolo = materiale.Titolo + EstensioneDaContentType(materiale.Tipo);
            return File(materiale.Materiale, materiale.Tipo, materiale.Titolo);
        }
        
        public async Task<IActionResult> View(Guid id)
        {
            var materiale = await dbContext.materiali.FindAsync(id);
            if (materiale == null || materiale.Materiale == null)
                return NotFound();

            return File(materiale.Materiale, materiale.Tipo);
        }

    }
}
