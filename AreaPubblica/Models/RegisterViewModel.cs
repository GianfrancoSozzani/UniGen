using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using AreaPubblica.Data;
using AreaPubblica.Models.Entities;
using AreaPubblica.Attributes;

namespace AreaPubblica.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato email non valido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria")]
        [MinLength(5, ErrorMessage = "La password deve contenere almeno 5 caratteri")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [RegularExpression(@"^[A-Z][a-z]+(?:\s[A-Z][a-z]+)*$", ErrorMessage = "Il nome deve iniziare con lettera maiuscola.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        [RegularExpression(@"^[A-Z][a-z]+(?:\s[A-Z][a-z]+)*$", ErrorMessage = "Il cognome deve iniziare con lettera maiuscola.")]
        public string? Cognome { get; set; }

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

        [Display(Name = "Immagine profilo")]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? ImmagineFile { get; set; } // File caricato dal form

        public byte[]? ImmagineProfilo { get; set; } // Da popolare nel controller
        public string? Tipo { get; set; } // Estensione (es: .jpg)
    }
}
