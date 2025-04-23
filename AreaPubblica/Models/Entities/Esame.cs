using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AreaPubblica.Models.Entities
{
    public class Esame
    {
        [Key]
        public Guid K_Esame { get; set; }
        public string TitoloEsame { get; set; }
        public Guid K_Docente { get; set; }
        [ForeignKey("K_Docente")]
        [ValidateNever]
        public Docente Docente { get; set; }
        public Guid K_Corso { get; set; }
        [ForeignKey("K_Corso")]
        [ValidateNever]
        public Corso Corso { get; set; }
        public byte[] CFU { get; set; }

    }
}
