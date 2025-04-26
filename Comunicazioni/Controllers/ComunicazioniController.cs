using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using LibreriaClassi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<IActionResult> List()
        {

            string ruolo = HttpContext.Session.GetString("ruolo");

            if (ruolo == "Studente")
            {
                var pianoDiStudi = await dbContext.PianiStudioPersonali
                    .Where(p => p.K_Studente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                    .Select(p => p.K_Esame)
                    .Distinct()
                    .ToListAsync();

                var comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Include(c => c.Esami)
                    .Where(c => pianoDiStudi.Contains(c.K_Esame) || c.K_Esame == null)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                return View(comunicazioni);
            }
            else if (ruolo == "Docente")
            {
                var Esami = await dbContext.Esami
                   .Where(p => p.K_Docente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                   .Select(p => p.K_Esame)
                   .Distinct()
                   .ToListAsync();

                var comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Include(c => c.Esami)
                    .Where(c => c.K_Esame.HasValue && Esami.Contains(c.K_Esame.Value) || c.K_Esame == null)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                return View(comunicazioni);
            }
            else
            {
                var comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Include(c => c.Esami)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                return View(comunicazioni);
            }
        }


        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        public void PopolaEsami()
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami.Select(i => new SelectListItem
            {
                Text = i.TitoloEsame,
                Value = i.K_Esame.ToString()
            });
            ViewBag.EsamiList = listaEsami;
        }

        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            PopolaEsami();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddComunicazioneViewModel viewModel)
        {
            string ruolo = HttpContext.Session.GetString("ruolo");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = Guid.NewGuid(),
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave")),
                K_Esame = viewModel.K_Esame
            };

            if (ruolo == "Operatore")
            {
                comunicazione.Soggetto = "A";
            }
            else if (ruolo == "Docente")
            {
                comunicazione.Soggetto = "D";
                comunicazione.K_Docente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }
            else if (ruolo == "Studente")
            {
                comunicazione.Soggetto = "S";
                comunicazione.K_Studente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join
            comunicazione.Esami = dbContext.Esami.FirstOrDefault(e => e.K_Esame == comunicazione.K_Esame);

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Comunicazioni");
        }


        public async Task<IActionResult> AddRisposta(Comunicazione viewModel)
        {
            string ruolo = HttpContext.Session.GetString("ruolo");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = viewModel.Codice_Comunicazione,
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave")),
                K_Esame = viewModel.K_Esame
            };

            if (ruolo == "Operatore")
            {
                comunicazione.Soggetto = "A";
            }
            else if (ruolo == "Docente")
            {
                comunicazione.Soggetto = "D";
                comunicazione.K_Docente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }
            else if (ruolo == "Studente")
            {
                comunicazione.Soggetto = "S";
                comunicazione.K_Studente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join
            comunicazione.Esami = dbContext.Esami.FirstOrDefault(e => e.K_Esame == comunicazione.K_Esame);

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Comunicazioni");
        }
    }
}
