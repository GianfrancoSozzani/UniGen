using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AreaStudente.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Faq()
        {
            var studenteIdStr = HttpContext.Session.GetString("cod");
            ViewData["studente_id"] = studenteIdStr;
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

