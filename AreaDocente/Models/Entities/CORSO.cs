using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class CORSO
    {
        [Key]
        public Guid K_Corso { get; set; }
        public Guid K_Facolta { get; set; }
        [ForeignKey("K_Facolta")]
        public FACOLTA FACOLTA { get; set; }
        public Guid K_TipoCorso { get; set; }
        [ForeignKey("K_TipoCorso")]
        public TIPOCORSO TIPO_CORSO { get; set; }
        public string TitoloCorso { get; set; }
        public string MinimoCFU { get; set; }
        public decimal CostoAnnuale { get; set; }
    }
}
