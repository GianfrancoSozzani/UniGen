using System.Dynamic;
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

        public IActionResult CorsiLaurea()
        {
            var corsi = dbContext.Corsi.ToList();
            return View(corsi);
        }

        public IActionResult CorsiLaureaConFacoltà()
        {
            var corsi = dbContext.Corsi
                .Join(
                dbContext.Corsi,
                corso => corso.K_Corso,
                facolta => facolta.K_Facolta,
               
                (corso, facolta) => new
                {
                    TitoloCorso = corso.TitoloCorso,
                    NomeFacolta = facolta.Facolta
                   
                })
             .ToList();
            var risultato = new List<dynamic>();
            foreach (var corso in corsi)
            {
                dynamic expando = new ExpandoObject();
                expando.TitoloCorso = corso.TitoloCorso;
                expando.Facolta = corso.NomeFacolta;
                risultato.Add(expando);
            }
            ViewBag.Dati = risultato;
            return View();
        }






        public IActionResult ElencoDocenti()
        {
            var docenti = dbContext.Docenti.ToList();
            return View(docenti);
        }
        public IActionResult ElencoDocentiConEsami()
        {
            var query = dbContext.Docenti
       .Join(
           dbContext.Esami,
           docente => docente.K_Docente,
           esame => esame.K_Docente,
           (docente, esame) => new
           {
               Nome = docente.Nome,
               Cognome = docente.Cognome,
               TitoloEsame = esame.TitoloEsame
           })
       .ToList();
            var risultato = new List<dynamic>();

            foreach (var item in query)
            {
                dynamic expando = new ExpandoObject();
                expando.Nome = item.Nome;
                expando.Cognome = item.Cognome;
                expando.TitoloEsame = item.TitoloEsame;
                risultato.Add(expando);
            }
            ViewBag.Dati = risultato;
            return View();
        }

    }
}
