using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AreaPubblica.Models.Entities
{
    public class PianoStudio
    {
        [Key]
        public Guid K_PianoStudio { get; set; }
        public Guid K_Corso { get; set; }
        [ForeignKey("K_Corso")]
        [ValidateNever]
        public Corso Corso { get; set; }
        public Guid K_Esame { get; set; }
        [ForeignKey("K_Facolta")]
        [ValidateNever]
        public Esame Esame { get; set; }
        public string AnnoAccademico { get; set; }
        public string Obbligatorio { get; set; }
    }
}
