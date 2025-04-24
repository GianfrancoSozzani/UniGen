using System.ComponentModel.DataAnnotations;

namespace AreaStudente.Models.Entities
{
    public class Docente
    {
        [Key]
        public Guid K_Docente { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
    }
}
