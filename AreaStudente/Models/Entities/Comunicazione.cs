using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaStudente.Models.Entities
{
    public class Comunicazione
    {
        [Key]
        public Guid K_Comunicazione { get; set; }
        public Guid Codice_Comunicazione { get; set; }
        public DateTime DataOraComunciazione { get; set; }
        public string? Soggetto { get; set; }
        public string? Testo { get; set; }
        public Guid K_Studente { get; set; }
        [ForeignKey]
        [ValidateNever]
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public Guid? K_Docente { get; set; }
    }
}
