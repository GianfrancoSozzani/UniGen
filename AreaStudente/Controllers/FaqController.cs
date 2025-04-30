using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AreaStudente.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Faq()
        {
            ViewData["studente_id"] = HttpContext.Session.GetString("studente_id");
            var studenteIdStr = HttpContext.Session.GetString("studente_id");
            if (Guid.TryParse(studenteIdStr, out Guid studenteId))
            {
                ViewBag.StudenteId = studenteId;
            }
            else
            {
                ViewBag.StudenteId = "ID non trovato nella sessione.";
            }
            return View();
        }
    }
}

