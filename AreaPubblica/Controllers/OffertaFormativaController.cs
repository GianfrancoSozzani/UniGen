using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using AreaPubblica.Data;
using AreaPubblica.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AreaPubblica.Controllers
{
    public class OffertaFormativaController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public OffertaFormativaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult ElencoCorsiCompleti()
        {
            var dati = (
                from corso in dbContext.Corsi
                join facolta in dbContext.Facolta on corso.K_Facolta equals facolta.K_Facolta
                join tipo in dbContext.TipiCorsi on corso.K_TipoCorso equals tipo.K_TipoCorso
                select new
                {
                    TitoloCorso = corso.TitoloCorso,
                    NomeFacolta = facolta.TitoloFacolta,
                    TipoCorso = tipo.Tipo,
                    Durata = tipo.Durata
                }
            ).ToList();

            ViewBag.Corsi = dati;

            return View();
        }





        public IActionResult ElencoDocenti()
        {
            var docenti = dbContext.Docenti.ToList();
            return View(docenti);
        }
        public IActionResult ElencoDocentiConEsami()
        {
            var dati = (
                from facolta in dbContext.Facolta
                join corso in dbContext.Corsi on facolta.K_Facolta equals corso.K_Facolta
                join piano in dbContext.PianiStudio on corso.K_Corso equals piano.K_Corso
                join esame in dbContext.Esami on piano.K_Esame equals esame.K_Esame
                join docente in dbContext.Docenti on esame.K_Docente equals docente.K_Docente
                select new
                {
                    Facolta = facolta.TitoloFacolta,
                    Corso = corso.TitoloCorso,
                    Esame = esame.TitoloEsame,
                    NomeDocente = docente.Nome,
                    CognomeDocente = docente.Cognome
                }
            ).ToList();

            ViewBag.FacoltaCorsiEsami = dati;

            return View();
        }
        public IActionResult Programma()
        { 
            return View();
        }
        
    }
}
