using AreaDocente.Data;
using AreaDocente.Models;
using AreaDocente.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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
        //ADD-------------------------------------------//
        //----------------------------------------------//
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


            Guid docenteId = Guid.Parse(HttpContext.Session.GetString("cod"));

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
                .OrderBy(s => s.Cognome)
                .Select(s => new SelectListItem
                {
                    Text = s.Cognome + " " + s.Nome,
                    Value = s.K_Studente.ToString()
                })
                .ToList(); // Esegui subito la query e materializza i risultati

            ViewBag.StudentiList = listaStudenti;

        }



        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            PopolaEsami(null);
            PopolaStudenti();
            return View();
        }



        [HttpPost]
        public IActionResult AddStudente(Guid? K_Esame)
        {
            PopolaEsami(null);
            PopolaStudenti();
            return View("Index", "Home");
        }


        //metodo per inviare email alla creazione di una comunicazione

        private async Task InvioEmail(Comunicazione comunicazione, string ruolo, string subject)
        {
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
                using (SmtpClient smtpClient = new SmtpClient("mail.brovia.it", 587))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");
                    smtpClient.EnableSsl = true; // Generalmente è meglio usare sempre SSL

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("generation@brovia.it", "Comunicazione UniGen");

                        foreach (var email in destinatariEmail.Distinct())
                        {
                            mail.To.Add(new MailAddress(email));
                        }

                        mail.Subject = subject;
                        string body = "";

                        if (ruolo == "d")
                        {
                            var docente = await dbContext.docenti.FirstOrDefaultAsync(d => d.K_Docente == comunicazione.K_Soggetto);
                            body = @$"In data {comunicazione.DataOraComunicazione}
hai ricevuto una comunicazione da {docente?.Nome} {docente?.Cognome}.

{comunicazione.Testo}";
                        }
                        else if (ruolo == "s")
                        {
                            var studente = await dbContext.studenti.FirstOrDefaultAsync(s => s.K_Studente == comunicazione.K_Soggetto);
                            body = @$"In data {comunicazione.DataOraComunicazione}
hai ricevuto una comunicazione da {studente?.Nome} {studente?.Cognome}.

{comunicazione.Testo}";
                        }
                        else
                        {
                            body = @$"In data {comunicazione.DataOraComunicazione}
hai ricevuto una comunicazione dall'Amministrazione.

{comunicazione.Testo}";
                        }
                        mail.Body = body;

                        try
                        {
                            await smtpClient.SendMailAsync(mail);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Errore invio email: " + ex.Message);
                        }
                    }
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(ListAndAddViewModel listAndAddViewModel)
        {
            var viewModel = listAndAddViewModel.AddComunicazione;
            string ruolo = HttpContext.Session.GetString("r");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = Guid.NewGuid(),
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo.Trim(),
                K_Studente = viewModel.K_Studente,
                K_Docente = viewModel.K_Docente
            };

            comunicazione.K_Soggetto = Guid.Parse(HttpContext.Session.GetString("cod"));
            comunicazione.K_Docente = null;
            comunicazione.Soggetto = "D";
            if (viewModel.K_Studente == null || viewModel.K_Studente == Guid.Empty)  // Se "Amministrazione"
            {
                comunicazione.K_Studente = null;  // Non associato a uno studente
            }



            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            await InvioEmail(comunicazione, ruolo, "Nuova comunicazione");
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> AddRisposta(Comunicazione viewModel)
        {
            string ruolo = HttpContext.Session.GetString("r");
            Guid chiaveUtente;


            chiaveUtente = Guid.Parse(HttpContext.Session.GetString("cod"));


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
                Testo = viewModel.Testo.Trim(),
                K_Soggetto = chiaveUtente,
            };


            nuovaRisposta.Soggetto = "D";
            nuovaRisposta.K_Studente = ultimaComunicazione.K_Studente;



            // Assegna chi riceverà la risposta
            nuovaRisposta.Docente = dbContext.docenti.FirstOrDefault(d => d.K_Docente == nuovaRisposta.K_Docente);
            nuovaRisposta.Studente = dbContext.studenti.FirstOrDefault(s => s.K_Studente == nuovaRisposta.K_Studente);

            await dbContext.Comunicazioni.AddAsync(nuovaRisposta);
            await dbContext.SaveChangesAsync();
            await InvioEmail(nuovaRisposta, ruolo, "Nuova risposta alla tua comunicazione");

            return RedirectToAction("Index", "Home");
        }
    }
}

