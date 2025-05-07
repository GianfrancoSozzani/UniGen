using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AreaDocente.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<HomeController> _logger;

        // Un solo costruttore che accetta entrambi i parametri
        public HomeController(ApplicationDbContext dbContext, ILogger<HomeController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }
        //parte per comunicazioni 
        public void PopolaEsami(Guid? IDEsame)
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.esami
                .Where(e => e.K_Docente == Guid.Parse(HttpContext.Session.GetString("cod")))
                .Select(e => new SelectListItem
                {
                    Text = e.TitoloEsame,
                    Value = e.K_Esame.ToString(),
                    Selected = (IDEsame.HasValue && e.K_Esame == IDEsame.Value)
                });
            ViewBag.EsamiList = listaEsami;
        }

        public void PopolaStudenti()
        {
            string ruolo = HttpContext.Session.GetString("r");


            Guid? docenteId = Guid.TryParse(HttpContext.Session.GetString("cod"), out Guid result) ? result : (Guid?)null;

            // Ottieni gli ID degli esami del docente
            var esamiDelDocenteList = dbContext.esami
                .Where(e => e.K_Docente == docenteId)
                .Select(e => e.K_Esame)
                .ToList(); // Esegui subito la query e materializza i risultati

            // Ottieni solo gli studenti che hanno almeno un esame del docente in PianiStudioPersonali
            var studentiFiltrati = dbContext.PianiStudioPersonali
                .Where(ps => ps.K_Esame.HasValue && esamiDelDocenteList.Contains(ps.K_Esame.Value))
                .Select(ps => ps.K_Studente)
                .Distinct()
                .ToList(); // Esegui subito la query e materializza i risultati

            // Carica solo questi studenti dalla tabella Studenti
            var listaStudenti = dbContext.studenti
                .Where(s => studentiFiltrati.Contains(s.K_Studente) && s.Matricola != null)
                .Select(s => new SelectListItem
                {
                    Text = s.Nome + " " + s.Cognome,
                    Value = s.K_Studente.ToString()
                })
                .ToList(); // Esegui subito la query e materializza i risultati

            ViewBag.StudentiList = listaStudenti;

        }
        //------------------------------------------//
        public IActionResult Index()
        {
            string? cod = HttpContext.Request.Query["cod"];
            string? usr = HttpContext.Request.Query["usr"];
            string? r = HttpContext.Request.Query["r"];

            if (cod != null && usr != null && r != null)
            {
                HttpContext.Session.SetString("cod", cod);
                HttpContext.Session.SetString("usr", usr);
                HttpContext.Session.SetString("r", r);
            }

            //return View();
            string ruolo = HttpContext.Session.GetString("r");

            // Recupera le comunicazioni in base al ruolo
            List<IGrouping<Guid, Comunicazione>> comunicazioni = new List<IGrouping<Guid, Comunicazione>>();

            // Popola gli esami e gli studenti per il docente
            PopolaEsami(null);
            PopolaStudenti();

            Guid? docente_chiave = Guid.TryParse(HttpContext.Session.GetString("cod"), out var result) ? result : (Guid?)null;


            // Se il ruolo è Docente, recupera le comunicazioni per il docente
            comunicazioni = dbContext.Comunicazioni
                .Where(c => c.K_Docente == docente_chiave || c.K_Soggetto == docente_chiave)
                .OrderBy(c => c.DataOraComunicazione)
                .GroupBy(c => c.Codice_Comunicazione)
                .ToList();


            // Aggiungi informazioni sui mittenti
            foreach (var gruppo in comunicazioni)
            {
                foreach (var comunicazione in gruppo)
                {
                    // Carica il mittente (se è un docente)
                    if (comunicazione.K_Soggetto.HasValue)
                    {
                        comunicazione.Docente = dbContext.docenti
                            .FirstOrDefault(d => d.K_Docente == comunicazione.K_Soggetto);
                    }
                    // Carica il mittente (se è uno studente) 
                    if (comunicazione.K_Soggetto.HasValue && comunicazione.Docente == null)
                    {
                        comunicazione.Studente = dbContext.studenti
                            .FirstOrDefault(s => s.K_Studente == comunicazione.K_Soggetto);
                    }
                }
            }

            // Crea il modello da passare alla vista
            var model = new ListAndAddViewModel
            {
                Comunicazioni = comunicazioni,  // Passa le comunicazioni recuperate
                AddComunicazione = new AddComunicazioneViewModel()
            };

            // Restituisci la vista con il modello
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contatti()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult ComeFunziona()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("https://localhost:7272");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
