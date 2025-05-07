using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AreaStudente.Models.Attributes;
using AreaStudente.Models.Entities;

namespace AreaStudente.Models
{
    public class ImmatricolazioneViewModel
    {
        [Key]
        public Guid K_Studente { get; set; }

        public string? Email { get; set; }

        public string? PWD { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        [RegularExpression(@"^[A-Z][a-z]+(?:\s[A-Z][a-z]+)*$", ErrorMessage = "Il cognome deve iniziare con lettera maiuscola.")]
        public string? Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [RegularExpression(@"^[A-Z][a-z]+(?:\s[A-Z][a-z]+)*$", ErrorMessage = "Il nome deve iniziare con lettera maiuscola.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria")]
        [DataType(DataType.Date)]
        public DateTime? DataNascita { get; set; }

        [Required(ErrorMessage = "L'indirizzo è obbligatorio")]
        public string? Indirizzo { get; set; }

        [Required(ErrorMessage = "Il CAP è obbligatorio")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Il CAP deve contenere esattamente 5 cifre.")]
        public string? CAP { get; set; }

        [Required(ErrorMessage = "La città è obbligatoria")]
        public string? Citta { get; set; }

        [Required(ErrorMessage = "La provincia è obbligatoria")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "La provincia deve contenere esattamente due lettere maiuscole.")]
        public string? Provincia { get; set; }
        //ogni volta che carichiamo una immagine dobbiamo avere il tipo di immagine
        public byte[]? ImmagineProfilo { get; set; }
        // nuovo campo per il file caricato
        [Display(Name = "Immagine profilo")]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
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
        public Corso? Corso { get; set; }

        public decimal? Importo { get; set; }
    }
}
