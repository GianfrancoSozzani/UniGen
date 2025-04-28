using System.Linq;
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
                //prendo gli esami del piano di studi dello studente
                var pianoDiStudi = await dbContext.PianiStudioPersonali
                    .Where(p => p.K_Studente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                    .Select(p => p.K_Esame)
                    .Distinct()
                    .ToListAsync();
                //prendo le comunicazioni relative agli esami del piano di studi e quelle dell'Amministrazione
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
                //prendo gli esami del docente
                var Esami = await dbContext.Esami
                   .Where(p => p.K_Docente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                   .Select(p => p.K_Esame)
                   .Distinct()
                   .ToListAsync();
                //prendo le comunicazioni relative agli esami del docente (quelle scritte da lui quindi e le risposte sotto)
                //+ prendo le comunicazioni dell'Amministrazione
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
                //se è un operatore dell'Amministrzione prendo tutte le comunicazioni
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
        public void  PopolaEsami(Guid? IDEsame)
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami
                .Where(e => e.K_Docente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                .Select(e => new SelectListItem
                {
                    Text = e.TitoloEsame,
                    Value = e.K_Esame.ToString(),
                    Selected = (IDEsame.HasValue && e.K_Esame == IDEsame.Value)
                });
            ViewBag.EsamiList = listaEsami;
        }

        public void PopolaStudenti(Guid? K_Esame)
        {
            string ruolo = HttpContext.Session.GetString("ruolo");
            if (ruolo == "Operatore")
            {
                IEnumerable<SelectListItem> listaStudenti = dbContext.Studenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Studente.ToString()
                    });
                ViewBag.StudentiList = listaStudenti;
            }
            else if (ruolo == "Docente" && K_Esame.HasValue)
            {
                var pianiDiStudio = dbContext.PianiStudioPersonali
                .Where(ps => ps.K_Esame == K_Esame.Value)
                .Select(ps => ps.K_Studente); // Ottieni solo gli ID degli studenti

                // Recupera gli studenti che hanno un K_Studente presente nei piani di studio trovati
                IEnumerable<SelectListItem> listaStudenti = dbContext.Studenti
                    .Where(s => pianiDiStudio.Contains(s.K_Studente))
                    .Select(s => new SelectListItem
                    {
                        Text = s.Nome + " " + s.Cognome,
                        Value = s.K_Studente.ToString()
                    });
                ViewBag.StudentiList = listaStudenti;
            }
        }

        public void PopolaDocenti()
        {
            string ruolo = HttpContext.Session.GetString("ruolo");
            if (ruolo == "Operatore")
            {
                IEnumerable<SelectListItem> listaDocenti = dbContext.Docenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Docente.ToString()
                    });
                ViewBag.DocentiList = listaDocenti;
            }
            else if (ruolo == "Studente")
            {
                var Idstudente = Guid.Parse(HttpContext.Session.GetString("chiave"));
                var pianiDiStudio = dbContext.PianiStudioPersonali
                .Where(ps => ps.K_Studente == Idstudente)
                .Select(ps => ps.K_Esame);
               
                var listaDocenti = dbContext.Esami
                       .Where(e => pianiDiStudio.Contains(e.K_Esame))
                       .Select(e => e.Docente)  
                       .Distinct()  
                       .Select(docente => new SelectListItem
                       {
                           Text = docente.Nome + " " + docente.Cognome,
                           Value = docente.K_Docente.ToString()
                       }).ToList();  // Converti in lista

                ViewBag.DocentiList = listaDocenti;
            }
        }


        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            PopolaEsami(null);
            PopolaStudenti(null);
            PopolaDocenti();
            return View();
        }

        [HttpPost]
        public IActionResult AddStudente(Guid? K_Esame) // Riceve l'ID dell'esame selezionato
        {
            PopolaEsami(null);
            PopolaStudenti(K_Esame);
            return View("Add");
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
                K_Esame = viewModel.K_Esame,
                K_Studente = viewModel.K_Studente
            };

            if (ruolo == "Operatore")
            {
                comunicazione.Soggetto = "A";
                //aggiunta necessaria: di defaULT Guid? = 00000000-0000-0000-0000-0000-0000-0000, quindi non risultava null,
                // e il meccanismo di list si inceppava
                comunicazione.K_Esame = null;
            }
            else if (ruolo == "Docente")
            {
                comunicazione.Soggetto = "D";
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave"));
                if (comunicazione.K_Studente != null)
                {
                    comunicazione.K_Esame = null;
                }
            }
            else if (ruolo == "Studente")
            {
                comunicazione.Soggetto = "S";
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave"));
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
            //meccanismo per aggiungere una risposta a una comunicazione
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
                comunicazione.K_Esame = null;
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
