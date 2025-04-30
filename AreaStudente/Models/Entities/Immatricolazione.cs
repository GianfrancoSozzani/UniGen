using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaStudente.Models.Entities
{
    public class Immatricolazione
    {
        [Key]
        public Guid K_Documento { get; set; }
        public string? Titolo { get; set; }

        public byte[]? Documento { get; set; }
        public string? Tipo { get; set; }
        public Guid? K_Studente { get; set; }
        [ForeignKey("K_Studente")]
        //[ValidateNever]
        

    } 
}
