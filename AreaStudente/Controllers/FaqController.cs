using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using AreaStudente.Data;     // namespace del tuo DbContext
using AreaStudente.Models;   // namespace del tuo modello Studente

namespace AreaStudente.Controllers
{
    public class FaqController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FaqController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Faq()
        {
            var studenteIdStr = HttpContext.Session.GetString("cod");

            ViewData["studente_id"] = studenteIdStr;

            if (Guid.TryParse(studenteIdStr, out Guid studenteId))
            {
                ViewBag.StudenteId = studenteId;

                // Query al database
                var studente = _context.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);

                if (studente != null)
                {
                    ViewData["email"] = studente.Email;
                    ViewData["matricola"] = studente.Matricola;
                    ViewData["abilitato"] = studente.Abilitato;
                }
            }
            else
            {
                ViewBag.StudenteId = "ID non trovato nella sessione.";
            }

            return View();
        }
    }
}
