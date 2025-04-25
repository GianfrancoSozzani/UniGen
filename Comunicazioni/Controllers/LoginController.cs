using Comunicazioni.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comunicazioni.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login(string ruolo, string usr, string pwd)
        {
            int ruoloId = int.Parse(ruolo);
            if (ruoloId == 1)
            {
                var amministratore = dbContext.Operatori.FirstOrDefault(d => d.USR == usr && d.PWD == pwd);
                if (amministratore != null)
                {
                    HttpContext.Session.SetString("chiave", amministratore.K_Operatore.ToString());
                    HttpContext.Session.SetString("ruolo", "Operatore");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username o password errati");
                    return View();
                }
            }
            else if (ruoloId == 2)
            {
                var docente = dbContext.Docenti.FirstOrDefault(d => d.Email == usr && d.PWD == pwd);
                if (docente != null)
                {
                    HttpContext.Session.SetString("chiave", docente.K_Docente.ToString());
                    HttpContext.Session.SetString("ruolo", "Docente");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username o password errati");
                    return View();
                }
            }
            else if (ruoloId == 3)
            {
                var studente = dbContext.Studenti.FirstOrDefault(d => d.Email == usr && d.PWD == pwd);
                if (studente != null)
                {
                    HttpContext.Session.SetString("chiave", studente.K_Studente.ToString());
                    HttpContext.Session.SetString("ruolo", "Studente");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username o password errati");
                    return View();
                }
            }
            return View();
        }
    }

}
