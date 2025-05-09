using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using AreaPubblica.Data;
using AreaPubblica.Models;
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
            // Recupero i corsi includendo Facoltà e TipoCorso
            var corsi = dbContext.Corsi
                .Include(c => c.Facolta)
                .Include(c => c.TipoCorso)
                .ToList(); // ✅ Convertiamo in lista PRIMA della selezione

            // 🔍 Debug: controllo il tipo di MinimoCFU prima di utilizzarlo
            foreach (var corso in corsi)
            {
                Console.WriteLine($"MinimoCFU: {corso.MinimoCFU} - Tipo: {corso.MinimoCFU.GetType()}");
            }

            // Costruisco l'elenco dei corsi con conversioni sicure
            var dati = corsi.Select(corso => new
            {
                IdCorso = corso.K_Corso,
                TitoloCorso = corso.TitoloCorso ?? "Titolo non disponibile",
                NomeFacolta = corso.Facolta?.TitoloFacolta ?? "Facoltà non disponibile",
                TipoCorso = corso.TipoCorso?.Tipo ?? "Tipo non specificato",
                Durata = corso.TipoCorso?.Durata ?? "Durata sconosciuta",
                MinimoCFU = Convert.ToString((int)corso.MinimoCFU) // ✅ Conversione diretta da Int16 a int e poi a string
            }).ToList();

            ViewBag.Corsi = dati;

            return View();
        }


        public IActionResult Dettagli(Guid id)
        {
            var corso = dbContext.Corsi
                .Include(c => c.Facolta)
                .Include(c => c.TipoCorso)
                .FirstOrDefault(c => c.K_Corso == id);

            if (corso == null)
            {
                return NotFound("⚠️ Il corso selezionato non esiste.");
            }

            // 🔍 Lettura del file JSON per recuperare la descrizione del corso
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "descrizioniCorsi.json");
            if (!System.IO.File.Exists(path))
            {
                return NotFound("⚠️ Il file delle descrizioni non esiste.");
            }

            var json = System.IO.File.ReadAllText(path);
            var descrizioni = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            // 🔍 Recuperiamo la descrizione per il corso
            string descrizioneCorso = descrizioni.ContainsKey(corso.TitoloCorso) ? descrizioni[corso.TitoloCorso] : "Descrizione non disponibile.";

            var corsoViewModel = new CorsoDettagliViewModel
            {
                IdCorso = corso.K_Corso,
                TitoloCorso = corso.TitoloCorso,
                NomeFacolta = corso.Facolta?.TitoloFacolta,
                TipoCorso = corso.TipoCorso?.Tipo,
                Durata = corso.TipoCorso?.Durata,
                Descrizione = descrizioneCorso // ✅ Aggiunta della descrizione
            };

            return PartialView("_DettagliCorso", corsoViewModel);
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
        //public IActionResult Programma()
        //{
        //    var dati = (
        //        from facolta in dbContext.Facolta
        //        join corso in dbContext.Corsi on facolta.K_Facolta equals corso.K_Facolta
        //        join piano in dbContext.PianiStudio on corso.K_Corso equals piano.K_Corso
        //        join esame in dbContext.Esami on piano.K_Esame equals esame.K_Esame
        //        join docente in dbContext.Docenti on esame.K_Docente equals docente.K_Docente
        //        select new
        //        {
        //            Facolta = facolta.TitoloFacolta,
        //            Corso = corso.TitoloCorso,
        //            Esame = esame.TitoloEsame,
        //            NomeDocente = docente.Nome,
        //            CognomeDocente = docente.Cognome
        //        }
        //    ).ToList<dynamic>(); 

        //    ViewBag.FacoltaCorsi = dati;
        //    return View();
        //}

        public IActionResult Programma()
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
            ).ToList<dynamic>();

            // Lettura JSON con descrizioni corsi
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "descrizioniCorsi.json");
            var json = System.IO.File.ReadAllText(path);
            var descrizioni = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            ViewBag.FacoltaCorsi = dati;
            ViewBag.DescrizioniCorsi = descrizioni;
            return View();
        }
        public IActionResult PianoStudi()
        {
            var dati = (
                from facolta in dbContext.Facolta
                join corso in dbContext.Corsi on facolta.K_Facolta equals corso.K_Facolta
                join piano in dbContext.PianiStudio on corso.K_Corso equals piano.K_Corso
                join esame in dbContext.Esami on piano.K_Esame equals esame.K_Esame
                select new
                {
                    Facolta = facolta.TitoloFacolta,
                    Corso = corso.TitoloCorso,
                    Esame = esame.TitoloEsame,
                    AnnoAccademico = piano.AnnoAccademico,
                    Obbligatorio = piano.Obbligatorio,
                    CFU = (int)esame.CFU

                }
            ).ToList<dynamic>();

            ViewBag.PianiStudi = dati;
            return View();

        }
    }
}
