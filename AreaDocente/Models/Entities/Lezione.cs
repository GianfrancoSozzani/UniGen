using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models.Entities
{
    public class Lezione
    {
        [Key]
        public Guid K_Lezione { get; set; }
        public Guid K_Esame { get; set; }
        public string? Titolo { get; set; }
        public string? Video { get; set; }

    }
}
