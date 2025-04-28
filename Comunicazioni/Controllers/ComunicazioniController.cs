using System.Linq;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using Comunicazioni.Data;
using Comunicazioni.Models;
using Comunicazioni.Models.Entities;
using LibreriaClassi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comunicazioni.Controllers
{
    public class ComunicazioniController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private object logger;

        public ComunicazioniController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //----------------------------------------------//
        //LIST------------------------------------------//
        //----------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> List()
        {
            string ruolo = HttpContext.Session.GetString("ruolo");

            if (ruolo == "Studente")
            {
                //prendo gli esami del piano di studi dello studente
                var pianoDiStudi = await dbContext.PianiStudioPersonali
                    .Where(p => p.K_Studente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                    .Select(p => p.K_Esame)
                    .Distinct()
                    .ToListAsync();
                //prendo le comunicazioni relative agli esami del piano di studi e quelle dell'Amministrazione
                var comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Include(c => c.Esami)
                    .Where(c => pianoDiStudi.Contains(c.K_Esame) || c.K_Esame == null)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                return View(comunicazioni);
            }
            else if (ruolo == "Docente")
            {
                //prendo gli esami del docente
                var Esami = await dbContext.Esami
                   .Where(p => p.K_Docente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                   .Select(p => p.K_Esame)
                   .Distinct()
                   .ToListAsync();
                //prendo le comunicazioni relative agli esami del docente (quelle scritte da lui quindi e le risposte sotto)
                //+ prendo le comunicazioni dell'Amministrazione
                var comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Include(c => c.Esami)
                    .Where(c => c.K_Esame.HasValue && Esami.Contains(c.K_Esame.Value) || c.K_Esame == null)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                return View(comunicazioni);
            }
            else
            {
                //se è un operatore dell'Amministrzione prendo tutte le comunicazioni
                var comunicazioni = await dbContext.Comunicazioni
                    .Include(c => c.Studente)
                    .Include(c => c.Docente)
                    .Include(c => c.Esami)
                    .OrderBy(c => c.DataOraComunicazione)
                    .GroupBy(c => c.Codice_Comunicazione)
                    .ToListAsync();

                return View(comunicazioni);
            }
        }
        //----------------------------------------------//
        //ADD-------------------------------------------//
        //----------------------------------------------//
        public void PopolaEsami()
        {
            IEnumerable<SelectListItem> listaEsami = dbContext.Esami
                .Where(e => e.K_Docente == Guid.Parse(HttpContext.Session.GetString("chiave")))
                .Select(i => new SelectListItem
                {
                    Text = i.TitoloEsame,
                    Value = i.K_Esame.ToString()
                });
            ViewBag.EsamiList = listaEsami;
        }

        [HttpGet] //visualizzo i dati
        public IActionResult Add()
        {
            PopolaEsami();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddComunicazioneViewModel viewModel)
        {
            string ruolo = HttpContext.Session.GetString("ruolo");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = Guid.NewGuid(),
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave")),
                K_Esame = viewModel.K_Esame
            };

            if (ruolo == "Operatore")
            {
                comunicazione.Soggetto = "A";
                //aggiunta necessaria: di defaULT Guid? = 00000000-0000-0000-0000-0000-0000-0000, quindi non risultava null,
                // e il meccanismo di list si inceppava
                comunicazione.K_Esame = null;
            }
            else if (ruolo == "Docente")
            {
                comunicazione.Soggetto = "D";
                comunicazione.K_Docente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }
            else if (ruolo == "Studente")
            {
                comunicazione.Soggetto = "S";
                comunicazione.K_Studente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join
            comunicazione.Esami = dbContext.Esami.FirstOrDefault(e => e.K_Esame == comunicazione.K_Esame);

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();

            //chiariamo il ruolo e identifichiamo i relativi destinatari 
            //LIST vuota da riempire con i destinatari 
            List<string> destinatari = new List<string>();

            //Controllo del ruolo 
            //se ruolo è nullo o non corrisponde a uno di quelli considerati "RUOLO NON VALIDO"
            if (string.IsNullOrEmpty(ruolo) || !new[] { "Docente", "Studente", "Operatore" }.Contains(ruolo))
            {
                Console.WriteLine("Errore: ruolo non valido.");
                return Content("Ruolo non valido."); // Interrompe l'esecuzione del metodo
                /* return HttpNotFound();  // Restituisce un errore 404*/
            }

            if (ruolo == "Docente")
            {
                if (comunicazione.K_Esame != null)
                {
                    //Recupera la lista di studenti iscritti all'esame
                    //Se non ci sono piani studio associati all'esame, viene restituito un errore.
                    var pianiStudio = dbContext.PianiStudioPersonali.Where(p => p.K_Esame == comunicazione.K_Esame).ToList();
                    if (!pianiStudio.Any())
                    {
                        Console.WriteLine("Nessun piano studio trovato per l'esame.");
                        return Content("Ruolo non valido.");
                        /* return HttpNotFound();  // Restituisce un errore 404*/
                    }

                    var studenti = pianiStudio.Select(p => p.K_Studente).Distinct().ToList();

                    // Estrai le email degli studenti dal db se ci sono studenti iscritti
                    foreach (var studenteId in studenti)
                    {
                        var studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == studenteId);
                        if (studente != null && !string.IsNullOrEmpty(studente.Email))
                        {
                            // Usa Guid.TryParse per evitare il problema di parsing
                            Guid guid;
                            if (Guid.TryParse(studente.Email, out guid))
                            {
                                // Se il parsing ha successo, aggiungi l'email
                                destinatari.Add(studente.Email);
                            }
                            else
                            {
                                // Gestisci caso dove l'email non è un GUID valido (se necessario)
                                Console.WriteLine($"Attenzione: L'email dello studente '{studente.Email}' non è valida. Studente ID: {studente.K_Studente}");
                            }
                        }
                    }

                    //Recupera le email degli operatori e aggiungi alla lista 
                    var operatori = dbContext.Operatori.ToList();
                    foreach (var operatore in operatori)
                    {
                        if (operatore != null && !string.IsNullOrEmpty(operatore.USR))
                        {
                            destinatari.Add(operatore.USR);
                        }
                    }
                }
            }
            else if (ruolo == "Studente")
            {
                if (comunicazione.K_Esame != null)
                {
                    //Recupera l'email del docente
                    var docente = dbContext.Esami.FirstOrDefault(e => e.K_Esame == comunicazione.K_Esame)?.Docente?.Email;
                    if (!string.IsNullOrEmpty(docente))
                    {
                        // Usa Guid.TryParse anche per il docente se necessario
                        Guid guid;
                        if (Guid.TryParse(docente, out guid))
                        {
                            //Se il parsing ha successo, aggiungi l'email
                            destinatari.Add(docente);
                        }
                        else
                        {
                            // Gestisci caso dove l'email del docente non è un GUID valido (se necessario)
                            Console.WriteLine($"Attenzione: L'email del docente '{docente}' non è valida. Esame ID: {comunicazione.K_Esame}");
                        }
                    }
                }

                // Aggiungi gli studenti iscritti al corso (escluso lo studente che invia)
                var studenti = dbContext.Studenti.Where(s => s.K_Corso == comunicazione.K_Esame && s.K_Studente != comunicazione.K_Studente).ToList();
                foreach (var studente in studenti)
                {
                    if (!string.IsNullOrEmpty(studente.Email))
                    {
                        // Usa Guid.TryParse per evitare il problema di parsing
                        Guid guid;
                        if (Guid.TryParse(studente.Email, out guid))
                        {
                            // Se il parsing ha successo, aggiungi l'email
                            destinatari.Add(studente.Email);
                        }
                        else
                        {
                            // Gestisci caso dove l'email non è un GUID valido (se necessario)
                            Console.WriteLine($"Attenzione: L'email dello studente '{studente.Email}' non è valida. Studente ID: {studente.K_Studente}");
                        }
                    }
                }

                // Aggiungi gli operatori
                var operatori = dbContext.Operatori.ToList();
                foreach (var operatore in operatori)
                {
                    if (!string.IsNullOrEmpty(operatore.USR))
                    {
                        destinatari.Add(operatore.USR);
                    }
                }
            }

            else if (ruolo == "Operatore")
            {
                if (comunicazione.K_Studente == null && comunicazione.K_Docente == null)
                {
                    // Aggiungi tutte le email di studenti e docenti
                    var studenti = dbContext.Studenti.ToList();
                    foreach (var studente in studenti)
                    {
                        if (!string.IsNullOrEmpty(studente.Email))
                        {
                            // Usa Guid.TryParse se necessario per l'email
                            Guid guid;
                            if (Guid.TryParse(studente.Email, out guid))
                            {
                                destinatari.Add(studente.Email);
                            }
                        }
                    }

                    var docenti = dbContext.Docenti.ToList();
                    foreach (var docente in docenti)
                    {
                        if (!string.IsNullOrEmpty(docente.Email))
                        {
                            // Usa Guid.TryParse per il docente se necessario
                            Guid guid;
                            if (Guid.TryParse(docente.Email, out guid))
                            {
                                destinatari.Add(docente.Email);
                            }
                        }
                    }
                }
                else
                {
                    if (comunicazione.K_Studente != null)
                    {
                        var studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente);
                        if (studente != null && !string.IsNullOrEmpty(studente.Email))
                        {
                            // Usa Guid.TryParse per l'email dello studente se necessario
                            Guid guid;
                            if (Guid.TryParse(studente.Email, out guid))
                            {
                                destinatari.Add(studente.Email);
                            }
                        }
                    }

                    if (comunicazione.K_Docente != null)
                    {
                        var docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente);
                        if (docente != null && !string.IsNullOrEmpty(docente.Email))
                        {
                            // Usa Guid.TryParse per l'email del docente se necessario
                            Guid guid;
                            if (Guid.TryParse(docente.Email, out guid))
                            {
                                destinatari.Add(docente.Email);
                            }
                        }
                    }
                }
            }



            //invia email

            //Se la lista ha destinatari viene creato un oggetto SmtpClient per inviare l'email tramite un server SMTP (mail.brovia.it).
            //Per ogni destinatario nella lista destinatari, viene aggiunto l'indirizzo email a mail.To.
            //Il corpo dell'email è il testo della comunicazione (comunicazione.Testo), e l'oggetto è "Nuova comunicazione".
            //Infine, si tenta di inviare l'email con il metodo smtpClient.Send(mail).
            if (destinatari.Any())
            {
                SmtpClient smtpClient = new SmtpClient("mail.brovia.it", 25);
                /*smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");*/
                smtpClient.Credentials = new System.Net.NetworkCredential("generation@brovia.it", "G3n3rat!on");
                smtpClient.EnableSsl = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("generation@brovia.it", "Sistema Comunicazioni");

                // Aggiungi destinatari dalla lista
                foreach (var email in destinatari.Distinct())
                {
                    mail.To.Add(new MailAddress(email));
                }

                // Aggiungi anche il tuo indirizzo email per il test
                mail.To.Add(new MailAddress("mffrso99@gmail.com"));  // Modifica con il tuo indirizzo

                mail.Subject = "Nuova comunicazione";
                mail.Body = comunicazione.Testo;

                try
                {
                    smtpClient.Send(mail); //Tentativo di inviare l'email
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore invio email: " + ex.Message); // Stampa l'errore sulla console
                }
            }



            return RedirectToAction("List", "Comunicazioni");
        }




        public async Task<IActionResult> AddRisposta(Comunicazione viewModel)
        {
            //meccanismo per aggiungere una risposta a una comunicazione
            string ruolo = HttpContext.Session.GetString("ruolo");

            var comunicazione = new Comunicazione
            {
                Codice_Comunicazione = viewModel.Codice_Comunicazione,
                DataOraComunicazione = DateTime.Now,
                Testo = viewModel.Testo,
                K_Soggetto = Guid.Parse(HttpContext.Session.GetString("chiave")),
                K_Esame = viewModel.K_Esame
            };

            if (ruolo == "Operatore")
            {
                comunicazione.Soggetto = "A";
                comunicazione.K_Esame = null;
            }
            else if (ruolo == "Docente")
            {
                comunicazione.Soggetto = "D";
                comunicazione.K_Docente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }
            else if (ruolo == "Studente")
            {
                comunicazione.Soggetto = "S";
                comunicazione.K_Studente = Guid.Parse(HttpContext.Session.GetString("chiave"));
            }

            //collegamento tra comunicazioni e studente-docente
            comunicazione.Studente = dbContext.Studenti.FirstOrDefault(s => s.K_Studente == comunicazione.K_Studente); //una join
            comunicazione.Docente = dbContext.Docenti.FirstOrDefault(d => d.K_Docente == comunicazione.K_Docente); //una join
            comunicazione.Esami = dbContext.Esami.FirstOrDefault(e => e.K_Esame == comunicazione.K_Esame);

            await dbContext.Comunicazioni.AddAsync(comunicazione);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Comunicazioni");
        }



    }
}
