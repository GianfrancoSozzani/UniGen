using AreaStudente.Data;
using AreaStudente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AreaStudente.Controllers
{
    public class StudentiController : Controller
    {

        private readonly ApplicationDbContext dbContext; // Sto staziando il contesto del database
        public StudentiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inizializzo il contesto del database 
        }

        [HttpGet]
        public async Task<IActionResult> Show(Guid id) // L'ID dello studente da visualizzare
        {
            // Trova lo studente includendo potenzialmente dati correlati se servissero
            // In questo caso, per il ViewModel fornito, non serve caricare il Corso,
            // ma lo lascio commentato come esempio se volessi il nome del corso in futuro.
            var studente = await dbContext.Studenti
                // Sostituisci con il tuo DbSet<Studente>
                                                    // .Include(s => s.Corso) // Esempio: Decommenta se hai una navigation property 'Corso' in Studente e vuoi il nome
                                          .FirstOrDefaultAsync(s => s.K_Studente == id);

            if (studente == null)
            {
                // Puoi personalizzare il messaggio o la pagina
                return NotFound($"Studente con ID {id} non trovato.");
            }

            // Mappa dall'entità Studente (dal DB) a ShowStudenteViewModel
            // Dentro l'action Show() nel StudentiController.cs, dopo aver recuperato 'studente'

            var viewModel = new ShowStudenteViewModel
            {
                K_Studente = studente.K_Studente,
                Email = studente.Email,
                Cognome = studente.Cognome,
                Nome = studente.Nome,
                DataNascita = studente.DataNascita,
                Indirizzo = studente.Indirizzo,
                CAP = studente.CAP,
                Citta = studente.Citta,
                Provincia = studente.Provincia,
                ImmagineProfilo = studente.ImmagineProfilo,
                Matricola = studente.Matricola,
                DataImmatricolazione = studente.DataImmatricolazione,
                K_Corso = studente.K_Corso,

                // Gestione di Abilitato (ora sappiamo che studente.Abilitato è string?)
                // Opzione 1: Passa direttamente la stringa, usa un default se è null
                Abilitato = studente.Abilitato ?? "Non specificato", // Se è null, mostra "Non specificato" (o "No", o "", scegli tu)

                // Opzione 2: Se vuoi comunque mostrare "Sì"/"No" basandoti sulla presenza di una stringa
                // Abilitato = !string.IsNullOrEmpty(studente.Abilitato) ? "Sì" : "No",

                // Opzione 3: Se la stringa nel DB ha valori specifici come "ATTIVO", "SOSPESO" etc.
                //            e vuoi mapparli a "Sì"/"No" o altro. Esempio:
                // Abilitato = studente.Abilitato?.ToUpper() switch
                // {
                //     "ATTIVO" => "Sì",
                //     "TRUE" => "Sì",
                //     "1" => "Sì",
                //     null => "Non specificato",
                //     _ => "No" // Tutti gli altri casi ("SOSPESO", "FALSE", "0", etc.)
                // },
            };


            return View(viewModel);
        }
    }
}
