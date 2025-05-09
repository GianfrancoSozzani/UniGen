using System.ComponentModel.DataAnnotations;

namespace AreaStudente.Models.Entities
{
    public class TipiCorsi
    {
        [Key]
        public Guid K_Tipo_Corso { get; set; }
        public string? Tipo { get; set; }
        public string? Durata { get; set; }
    }
}
