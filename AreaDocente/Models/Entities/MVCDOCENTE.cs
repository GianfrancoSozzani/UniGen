using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models.Entities
{
    public class MVCDOCENTE
    {
        [Key]
        public Guid K_Docente { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascita { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public IFormFile ImmagineProfilo { get; set; }
        public string Tipo { get; set; }
        public DateTime DataRegistrazione { get; set; }
        public char Abilitato { get; set; }
    }
}
