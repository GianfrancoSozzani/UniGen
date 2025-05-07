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

        [HttpPost]
        public async Task<ActionResult> Add(AddMaterialiViewModel viewModel)
        {
            // CONTROLLI FORMALI --------------------------------------------------//
            if (viewModel.Titolo == null)
            {
                TempData["ErrorMessage"] = "Titolo mancante!";
                PopolaEsami();
                return View(viewModel);
            }
            if (viewModel.materiale == null || viewModel.materiale.Length == 0)
            {
                TempData["ErrorMessage"] = "Materiale mancante!";
                PopolaEsami();
                return View(viewModel);
            }
            if (!viewModel.K_Esame.HasValue || viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                PopolaEsami();
                return View(viewModel);
            }
            //---------------------------------------------------------------------//

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
            PopolaEsami();

            var materiali = await dbContext.materiali
                .Include(a => a.esame)
                .Where(a => a.esame.K_Docente == new Guid(HttpContext.Session.GetString("cod")))
                .ToListAsync();

            return View(materiali);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMaterialiViewModel viewModel)
        {
            // CONTROLLI FORMALI --------------------------------------------------//
            if (viewModel.Titolo == null)
            {
                TempData["ErrorMessage"] = "Inserire un titolo!";
                PopolaEsami();
                return View(viewModel);
            }
            if (viewModel.K_Esame == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Selezionare un esame!";
                PopolaEsami();
                return View(viewModel);
            }
            //---------------------------------------------------------------------//

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
                // Documenti
                "application/pdf" => ".pdf",
                "application/msword" => ".doc",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
                "application/vnd.ms-excel" => ".xls",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ".xlsx",
                "application/vnd.ms-powerpoint" => ".ppt",
                "application/vnd.openxmlformats-officedocument.presentationml.presentation" => ".pptx",
                "text/plain" => ".txt",
                "text/csv" => ".csv",
                "application/rtf" => ".rtf",
                "application/xml" => ".xml",
                "application/json" => ".json",

                // Immagini
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "image/bmp" => ".bmp",
                "image/tiff" => ".tif",
                "image/webp" => ".webp",
                "image/svg+xml" => ".svg",
                "image/x-icon" => ".ico",

                // Audio
                "audio/mpeg" => ".mp3",
                "audio/wav" => ".wav",
                "audio/ogg" => ".ogg",
                "audio/webm" => ".weba",

                // Video
                "video/mp4" => ".mp4",
                "video/x-msvideo" => ".avi",
                "video/x-ms-wmv" => ".wmv",
                "video/webm" => ".webm",
                "video/quicktime" => ".mov",
                "video/mpeg" => ".mpeg",

                // Archivi
                "application/zip" => ".zip",
                "application/x-zip-compressed" => ".zip",
                "application/x-rar-compressed" => ".rar",
                "application/x-7z-compressed" => ".7z",
                "application/gzip" => ".gz",
                "application/x-tar" => ".tar",

                // HTML
                "text/html" => ".html",
                "application/xhtml+xml" => ".xhtml",

                // Font
                "font/woff" => ".woff",
                "font/woff2" => ".woff2",
                "application/font-woff" => ".woff",
                "application/vnd.ms-fontobject" => ".eot",
                "font/ttf" => ".ttf",
                "font/otf" => ".otf",

                // Fallback
                _ => ""
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
