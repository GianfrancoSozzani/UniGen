using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        [HttpPost]
        public async Task<ActionResult> Add(AddMaterialiViewModel viewModel ) 
        {
            var materiali = new Materiali
            {
                Titolo = viewModel.Titolo.ToString(),
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
            
            return View();
        }
    }
}
