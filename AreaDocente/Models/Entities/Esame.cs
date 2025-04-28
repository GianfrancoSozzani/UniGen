using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class Esame
    {
        [Key]
        public Guid K_Esame { get; set; }
        public Guid K_PianoStudio { get; set; }
        [ForeignKey("K_PianoStudio")]
        [ValidateNever]
        public string TitoloEsame { get; set; }
        public int CFU { get; set; }
        [ForeignKey("K_Docente")]
        [ValidateNever]
        public Guid K_Docente { get; set; }
    }
}
