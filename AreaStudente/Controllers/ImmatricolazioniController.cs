using AreaStudente.Data;
using AreaStudente.Models;
using AreaStudente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Enumeration;

namespace AreaStudente.Controllers
{
    public class ImmatricolazioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ImmatricolazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddImmatricolazioneViewModel viewModel, Guid id)
        {
            if (viewModel.DocumentoFormFile == null || viewModel.DocumentoFormFile.Length == 0)
            {
                TempData["AlertMessage"] = "Seleziona un file PDF.";
                return RedirectToAction("Add"); // Corretto: solo il nome dell'azione
            }

            if (viewModel.DocumentoFormFile.ContentType.ToLower() != "application/pdf")
            {
                TempData["AlertMessage"] = "Formato non valido. Sono accettati solo PDF";
                return RedirectToAction("Add"); // Corretto: solo il nome dell'azione
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.DocumentoFormFile.CopyToAsync(memoryStream);
                    ViewData["studente_id"] = id;
                    var studente = new Immatricolazione
                    {
                        Documento = memoryStream.ToArray(),
                        Titolo =viewModel.NomeFile.FileName(),
                        K_Studente = id, // Assicurati che sia corretto
                        Tipo = viewModel.Tipo // Assicurati che sia corretto
                        // ... assegna valori a tutte le altre proprietà necessarie di Immatricolazione
                    };

                    await dbContext.Immatricolazioni.AddAsync(studente);
                    await dbContext.SaveChangesAsync();

                    TempData["PopupSuccesso"] = "PDF caricato con successo.";
                    return RedirectToAction("Add"); // Corretto: solo il nome dell'azione
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Errore durante il salvataggio: {ex}"); // Log dell'errore
                TempData["AlertMessage"] = "Si è verificato un errore durante il caricamento del PDF. Riprova.";
                return RedirectToAction("Add"); // Corretto: solo il nome dell'azione
            }
        }
    }
}