using AreaStudente.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaStudente.Models
{
    public class AddComunicazioneViewModel
    {
        [Key]
        public Guid K_Comunicazione { get; set; }
        public Guid Codice_Comunicazione { get; set; }
        public DateTime? DataOraComunciazione { get; set; }
        public string? Soggetto { get; set; } = "s";
        public Guid K_Soggetto { get; set; }
        [ForeignKey("K_Soggetto")]
        [ValidateNever]
        public Soggetto? Nome { get; set; }
        public Soggetto? Cognome { get; set; }
        public string? Testo { get; set; }
        public Guid? K_Studente { get; set; } = null;
        public Guid? K_Docente { get; set; }
        [ForeignKey("K_Docente")]
        [ValidateNever]
        public Docente? NomeDocente { get; set; }
        public Docente? CognomeDocente { get; set; }
    }
}
