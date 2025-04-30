using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AreaStudente.Models
{
    public class AddImmatricolazioneViewModel
    {
        [Key]
        public Guid K_Documento { get; set; }
        public string? Titolo { get; set; }

        public byte[]? Documento { get; set; }
        public IFormFile? DocumentoFormFile { get; set; }
        public string? Tipo { get; set; }
        public Guid? K_Studente { get; set; }
        [ForeignKey("K_Studente")]
        [ValidateNever]
        public string? Cognome { get; set; }
    }
}
