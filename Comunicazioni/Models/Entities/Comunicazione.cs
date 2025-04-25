using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Comunicazioni.Models.Entities
{
    public class Comunicazione
    {
        [Key]
        public Guid K_Comunicazione { get; set; }
        public Guid Codice_Comunicazione { get; set; }
        public DateTime DataOraComunicazione { get; set; }
        public string? Soggetto { get; set; }
        public string? Testo { get; set; }
        public Guid? K_Studente { get; set; }
        [ForeignKey("K_Studente")]
        [ValidateNever]
        public Studente? Studente { get; set; }
        public Guid? K_Docente { get; set; }
        [ForeignKey("K_Docente")]
        [ValidateNever]
        public Docente? Docente{ get; set; }


    }
}
