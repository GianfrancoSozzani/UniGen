using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        [ForeignKey("K_Studente")]
        public Guid? K_Studente { get; set; }
        //[ValidateNever]
    }
}
