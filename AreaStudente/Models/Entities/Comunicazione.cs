using System.ComponentModel.DataAnnotations;

namespace AreaStudente.Models.Entities
{
    public class Comunicazione
    {
        [Key]
        public Guid K_Comunicazione { get; set; }
        public Guid? Codice_Comunicazione { get; set; }
        public DateTime? DataOraComunicazione { get; set; }
        public string? Soggetto { get; set; }
        public Guid? K_Soggetto { get; set; }
        public string? Testo { get; set; }
        public Guid K_Studente { get; set; }
        public Guid K_Docente { get; set; }

    }
}
