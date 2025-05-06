using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCValutazione
    {
        [Key]
        public Guid K_Valutazione { get; set; }
        public Guid? K_Studente { get; set; }

        [ForeignKey("K_Studente")]
        [ValidateNever]
        public MVCStudente? Studente { get; set; }
        public Guid? K_Prova { get; set; }

        [ForeignKey("K_Prova")]
        [ValidateNever]
        public MVCPROVA? Prova { get; set; }
    }
}
