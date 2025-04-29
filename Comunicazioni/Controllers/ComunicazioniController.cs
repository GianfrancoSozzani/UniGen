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
            string ruolo = HttpContext.Session.GetString("Ruolo");
            List<IGrouping<Guid, Comunicazione>> comunicazioni;

            if (ruolo == "S")
            {
                var studente_chiave = Guid.Parse(HttpContext.Session.GetString("K_Studente"));

                comunicazioni = await dbContext.Comunicazioni
                    .Where(c => c.K_Studente == studente_chiave || c.K_Soggetto == studente_chiave)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                foreach (var gruppo in comunicazioni)
                {
                    foreach (var comunicazione in gruppo)
                    {
                        // Carica il mittente (se è uno studente)
                        if (comunicazione.K_Soggetto.HasValue)
                        {
                            comunicazione.Studente = await dbContext.Studenti
                                .FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        }
                        // Carica il mittente (se è un docente) - Potrebbe essere necessario un controllo aggiuntivo se K_Soggetto può essere sia studente che docente
                        if (comunicazione.K_Soggetto.HasValue && comunicazione.Studente == null)
                        {
                            comunicazione.Docente = await dbContext.Docenti
                                .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                        }
                    }
                }

                return View(comunicazioni);
            }
            // ... Logica simile per il ruolo "Docente" e "Altro" ...
            else if (ruolo == "D")
            {
                var docente_chiave = Guid.Parse(HttpContext.Session.GetString("K_Docente"));

                comunicazioni = await dbContext.Comunicazioni
                    .Where(c => c.K_Docente == docente_chiave || c.K_Soggetto == docente_chiave)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                foreach (var gruppo in comunicazioni)
                {
                    foreach (var comunicazione in gruppo)
                    {
                        // Carica il mittente (se è un docente)
                        if (comunicazione.K_Soggetto.HasValue)
                        {
                            comunicazione.Docente = await dbContext.Docenti
                                .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                        }
                        // Carica il mittente (se è uno studente) 
                        if (comunicazione.K_Soggetto.HasValue && comunicazione.Docente == null)
                        {
                            comunicazione.Studente = await dbContext.Studenti
                                .FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        }
                    }
                }

                return View(comunicazioni);
            }
            else
            {

                // Recupera i Codice_Comunicazione dei messaggi inviati dall'amministrazione
                var codiciComunicazioneAmministrazione = await dbContext.Comunicazioni
                    .Where(c => c.K_Studente == null && c.K_Docente == null)
                    .Select(c => c.Codice_Comunicazione)
                    .Distinct()
                    .ToListAsync();

                // Recupera tutte le comunicazioni che hanno un Codice_Comunicazione appartenente alle comunicazioni amministrative
                comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Where(c => codiciComunicazioneAmministrazione.Contains(c.Codice_Comunicazione)) // Qui prendiamo sia le comunicazioni amministrative che le risposte
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                foreach (var gruppo in comunicazioni)
                {
                    foreach (var comunicazione in gruppo)
                    {
                        // Carica il mittente (se è un docente)
                        if (comunicazione.K_Soggetto.HasValue)
                        {
                            comunicazione.Docente = await dbContext.Docenti
                                .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                        }
                        // Carica il mittente (se è uno studente) 
                        if (comunicazione.K_Soggetto.HasValue && comunicazione.Docente == null)
                        {
                            comunicazione.Studente = await dbContext.Studenti
                                .FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                        }
                    }
                }

                return View(comunicazioni);

            }

        }

        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        public void PopolaEsami(Guid? IDEsame)
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami
                .Where(e => e.K_Docente == Guid.Parse(HttpContext.Session.GetString("K_Docente")))
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
            string ruolo = HttpContext.Session.GetString("Ruolo");
            if (ruolo == "O")
            {
                IEnumerable<SelectListItem> listaStudenti = dbContext.Studenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Studente.ToString()
                    });
                ViewBag.StudentiList = listaStudenti;
            }
            else if (ruolo == "D" && K_Esame.HasValue)
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
            string ruolo = HttpContext.Session.GetString("Ruolo");
            if (ruolo == "O")
            {
                IEnumerable<SelectListItem> listaDocenti = dbContext.Docenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Docente.ToString()
                    });
                ViewBag.DocentiList = listaDocenti;
            }
            else if (ruolo == "S")
            {
                var Idstudente = Guid.Parse(HttpContext.Session.GetString("K_Studente"));
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

            string ruolo = HttpContext.Session.GetString("Ruolo");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = Guid.NewGuid(),
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Studente = viewModel.K_Studente,
                K_Docente = viewModel.K_Docente
            };

            if (ruolo == "O")
            {
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("K_Operatore"));
                comunicazione.Soggetto = "A";
                //aggiunta necessaria: di default Guid? = 00000000-0000-0000-0000-0000-0000-0000, quindi non risultava null,
                // e il meccanismo di list si inceppava

                if (comunicazione.K_Studente != null)
                {
                    comunicazione.K_Docente = null;
                }
                else if (comunicazione.K_Docente != null)
                {
                    comunicazione.K_Studente = null;
                }
            }
            else if (ruolo == "D")
            {
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("K_Docente"));
                comunicazione.K_Docente = null;
                comunicazione.Soggetto = "D";
                if (viewModel.K_Studente == null || viewModel.K_Studente.ToString() == "Amministrazione")  // Se "Amministrazione"
                {
                    comunicazione.K_Studente = null;  // Non associato a uno studente
                }
            }
            else if (ruolo == "S")
            {
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("K_Studente"));
                comunicazione.K_Studente = null;
                comunicazione.Soggetto = "S";
                if (viewModel.K_Docente == null || viewModel.K_Docente.ToString() == "Amministrazione")  // Se "Amministrazione"
                {
                    comunicazione.K_Docente = null;  // Non associato a uno studente
                }
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Comunicazioni");

        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddRisposta(Comunicazione viewModel)
        {
            string ruolo = HttpContext.Session.GetString("Ruolo");
            Guid chiaveUtente;

            if (ruolo == "S")
            {
                chiaveUtente = Guid.Parse(HttpContext.Session.GetString("K_Studente"));
            }
            else if (ruolo == "D")
            {
                chiaveUtente = Guid.Parse(HttpContext.Session.GetString("K_Docente"));
            }
            else
            {
                chiaveUtente = Guid.Parse(HttpContext.Session.GetString("K_Operatore"));
            }

            var ultimaComunicazione = dbContext.Comunicazioni
                .Where(c => c.Codice_Comunicazione == viewModel.Codice_Comunicazione)
                .OrderByDescending(c => c.DataOraComunicazione)
                .FirstOrDefault();

            if (ultimaComunicazione == null)
            {
                return BadRequest("Comunicazione non trovata.");
            }

            var nuovaRisposta = new Comunicazione
            {
                Codice_Comunicazione = viewModel.Codice_Comunicazione,
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Soggetto = chiaveUtente,
            };



            if (ruolo == "O")
            {
                nuovaRisposta.Soggetto = "A";

                // Mantiene il destinatario originale della conversazione
                if (ultimaComunicazione.K_Studente != null)
                {
                    nuovaRisposta.K_Studente = ultimaComunicazione.K_Studente;
                }
                else if (ultimaComunicazione.K_Docente != null)
                {
                    nuovaRisposta.K_Docente = ultimaComunicazione.K_Docente;
                }
            }


            else if (ruolo == "D")
            {
                nuovaRisposta.Soggetto = "D";
                nuovaRisposta.K_Studente = ultimaComunicazione.K_Studente;
            }
            else if (ruolo == "S")
            {
                nuovaRisposta.Soggetto = "S";
                nuovaRisposta.K_Docente = ultimaComunicazione.K_Docente;
            }

            // Assegna chi riceverà la risposta
            nuovaRisposta.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == nuovaRisposta.K_Docente);
            nuovaRisposta.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == nuovaRisposta.K_Studente);

            await dbContext.Comunicazioni.AddAsync(nuovaRisposta);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Comunicazioni");
        }
    }
}

