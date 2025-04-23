using Comunicazioni.Data;
using Microsoft.AspNetCore.Mvc;

namespace Comunicazioni.Controllers
{
    public class ComunicazioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ComunicazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        //----------------------------------------------//
        //LIST------------------------------------------//
        //----------------------------------------------//

        

    }
}
