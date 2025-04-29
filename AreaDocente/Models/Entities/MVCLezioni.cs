using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCLezioni
    {
        [Key]
        public Guid K_Lezione { get; set; }
        public Guid K_Esame { get; set; }
        [ForeignKey("K_Esame")]
        public MVCEsame? Esame { get; set; }
        public string? Titolo { get; set; }
        public string? Video { get; set; }
    }
}
