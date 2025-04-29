using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCEsame
    {
        [Key]
        public Guid K_Esame { get; set; }
        public string TitoloEsame { get; set; }
        public Guid K_Docente { get; set; }
        [ForeignKey("K_Docente")]
        public MVCDOCENTE Docente { get; set; }
        public byte CFU { get; set; }
    }
}
