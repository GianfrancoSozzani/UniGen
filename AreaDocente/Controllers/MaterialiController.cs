using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<ActionResult> Add(AddMaterialiViewModel viewModel)
        {
            var materiali = new MVCMateriali
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
        //public void PopolaEsame()
        //{
        //    IEnumerable<SelectListItem> ListaEsame = dbContext.Facolta.Select(i => new SelectListItem
        //    {
        //        Text = i.descrizione,
        //        Value = i.IdFacolta.ToString()
        //    });
        //    ViewBag.FacoltaList = ListaEsame;
        //}
        public ActionResult Add()
        {
            return View();
        }
    }
}
