using System.Linq;
using System.Net.Mail;
using AreaStudente.Data;
using AreaStudente.Models;
using AreaStudente.Models.Entities;
using LibreriaClassi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comunicazioni.Controllers
{
    public class ComunicazioniController : Controller
    {
        private readonly AreaStudente.Data.ApplicationDbContext dbContext;
        public ComunicazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        public void PopolaEsami(Guid? IDEsame)
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami
                .Where(e => e.K_Docente == Guid.Parse(HttpContext.Session.GetString("cod")))
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
            string ruolo = HttpContext.Session.GetString("r");
            if (ruolo == "a")
            {
                IEnumerable<SelectListItem> listaStudenti = dbContext.Studenti
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nome + " " + i.Cognome,
                        Value = i.K_Studente.ToString()
                    });
                ViewBag.StudentiList = listaStudenti;
            }
            else if (ruolo == "d" && K_Esame.HasValue)
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

        //public void PopolaDocenti()
        //{
        //    string ruolo = HttpContext.Session.GetString("r");
        //    if (ruolo == "a")
        //    {
        //        IEnumerable<SelectListItem> listaDocenti = dbContext.Docenti
        //            .Select(i => new SelectListItem
        //            {
        //                Text = i.Nome + " " + i.Cognome,
        //                Value = i.K_Docente.ToString()
        //            });
        //        ViewBag.DocentiList = listaDocenti;
        //    }
        //    else if (ruolo == "s")
        //    {
        //        var Idstudente = Guid.Parse(HttpContext.Session.GetString("cod"));
        //        var pianiDiStudio = dbContext.PianiStudioPersonali
        //        .Where(ps => ps.K_Studente == Idstudente)
        //        .Select(ps => ps.K_Esame);

        //        var listaDocenti = dbContext.Esami
        //               .Where(e => pianiDiStudio.Contains(e.K_Esame))
        //               .Select(e => e.Docente)
        //               .Distinct()
        //               .Select(docente => new SelectListItem
        //               {
        //                   Text = docente.Nome + " " + docente.Cognome,
        //                   Value = docente.K_Docente.ToString()
        //               }).ToList();  // Converti in lista

        //        ViewBag.DocentiList = listaDocenti;
        //    }
        //}


        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            string ruolo = HttpContext.Session.GetString("r");
            var studenteIdStr = HttpContext.Session.GetString("cod");
            ViewData["studente_id"] = studenteIdStr; // per passare l'ID dello studente alla vista


            if (Guid.TryParse(studenteIdStr, out Guid studenteId))
            {
                var studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);
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
            PopolaEsami(null);
            PopolaStudenti(null);
            //PopolaDocenti();
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
        public async Task<IActionResult> Add(AreaStudente.Models.StudenteDashboardViewModel viewModel)
        {
           

            string ruolo = HttpContext.Session.GetString("r");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = Guid.NewGuid(),
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.NuovaComunicazione.Testo.Trim(),
                K_Studente = viewModel.NuovaComunicazione.K_Studente,
                K_Docente = viewModel.NuovaComunicazione.K_Docente
            };

            if (ruolo == "a")
            {
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("cod"));
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
            else if (ruolo == "d")
            {
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("cod"));
                comunicazione.K_Docente = null;
                comunicazione.Soggetto = "D";
                if (viewModel.NuovaComunicazione.K_Studente == null || viewModel.NuovaComunicazione.K_Studente == Guid.Empty)  // Se "Amministrazione"
                {
                    comunicazione.K_Studente = null;  // Non associato a uno studente
                }
            }
            else if (ruolo == "s")
            {
                comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("cod"));
                comunicazione.K_Studente = null;
                comunicazione.Soggetto = "S";
                if (viewModel.NuovaComunicazione.K_Docente == null || viewModel.NuovaComunicazione.K_Docente == Guid.Empty)  // Se "Amministrazione"
                {
                    comunicazione.K_Docente = null;  // Non associato a uno studente
                }
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();

            //------------------------------------------------------------------------------------------//
            //EMAIL

            List<string> destinatariEmail = new List<string>();

            // Determina i destinatari in base al ruolo e ai campi K_Studente/K_Docente
            if (ruolo == "a")
            {
                // L'operatore potrebbe inviare a uno studente o a un docente specifico
                if (comunicazione.K_Studente.HasValue && comunicazione.Studente?.Email != null)
                {
                    destinatariEmail.Add(comunicazione.Studente.Email);
                }
                else if (comunicazione.K_Docente.HasValue && comunicazione.Docente?.Email != null)
                {
                    destinatariEmail.Add(comunicazione.Docente.Email);
                }

            }
            else if (ruolo == "d")
            {
                // Il docente potrebbe inviare a uno studente specifico
                if (comunicazione.K_Studente.HasValue && comunicazione.Studente?.Email != null)
                {
                    destinatariEmail.Add(comunicazione.Studente.Email);
                }
                else
                {
                    destinatariEmail.Add("generation@brovia.it");
                }

            }
            else if (ruolo == "s")
            {
                // Lo studente potrebbe inviare a un docente specifico
                if (comunicazione.K_Docente.HasValue && comunicazione.Docente?.Email != null)
                {
                    destinatariEmail.Add(comunicazione.Docente.Email);
                }
                else
                {
                    destinatariEmail.Add("generation@brovia.it");
                }
            }

            // Invia email se ci sono destinatari
            if (destinatariEmail.Any())
            {
                SmtpClient smtpClient = new SmtpClient("mail.brovia.it", 587);
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");
                smtpClient.EnableSsl = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("generation@brovia.it", "Comunicazione UniGen");

                foreach (var email in destinatariEmail.Distinct())
                {
                    mail.To.Add(new MailAddress(email));
                }

                mail.Subject = "Nuova comunicazione";

                if (ruolo == "d")
                {
                    comunicazione.Docente = await dbContext.Docenti
                              .FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);

                    mail.Body = @$"In data {comunicazione.DataOraComunicazione}  
hai ricevuto una comunicazione da {comunicazione.Docente?.Nome} {comunicazione.Docente?.Cognome}. 

{comunicazione.Testo}";
                }
                else if (ruolo == "s")
                {
                    comunicazione.Studente = await dbContext.Studenti
                              .FirstOrDefaultAsync(d => d.K_Studente == comunicazione.K_Soggetto);

                    mail.Body = @$"In data {comunicazione.DataOraComunicazione}  
hai ricevuto una comunicazione da {comunicazione.Studente?.Nome} {comunicazione.Studente?.Cognome}. 

{comunicazione.Testo}";
                }
                else
                {
                    mail.Body = @$"In data {comunicazione.DataOraComunicazione}  
hai ricevuto una comunicazione dall'Amministrazione. 

{comunicazione.Testo}";
                }

                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore invio email: " + ex.Message);
                }
            }


            //---------------------------------------------------//



            return RedirectToAction("Show", "Studenti", new { cod = HttpContext.Session.GetString("cod") });

        }

        
    }
}

