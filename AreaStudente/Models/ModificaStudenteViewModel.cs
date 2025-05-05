using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AreaStudente.Models.Entities;
using LibreriaClassi;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AreaStudente.Models
{
    public class ModificaStudenteViewModel
    {
        [Key]
        public Guid K_Studente { get; set; }
        public string? Email { get; set; }
        public string? PWD { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string? Indirizzo { get; set; }
        public string? CAP { get; set; }
        public string? Citta { get; set; }
        public string? Provincia { get; set; }
        //ogni volta che carichiamo una immagine dobbiamo avere il tipo di immagine
        public byte[]? ImmagineProfilo { get; set; }
        // nuovo campo per il file caricato
        public IFormFile? ImmagineProfiloFile { get; set; }
        public string? Tipo { get; set; }
        public int? Matricola { get; set; }

        public DateTime? DataImmatricolazione { get; set; }
        public Guid? K_Facolta { get; set; } // selezione utente
        public IEnumerable<SelectListItem>? FacoltaList { get; set; } // per la DDL
        public Guid? K_Corso { get; set; }
        public IEnumerable<SelectListItem>? CorsiList { get; set; }
        [ForeignKey("K_Corso")]
        [ValidateNever]
        //public Facolta Facolta { get; set; }
        public Corso Corso { get; set; }

    }
}
