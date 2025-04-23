using System.ComponentModel.DataAnnotations;

namespace AreaPubblica.Models.Entities
{
    public class Docente
    {
        [Key]
        public Guid K_Docente { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public byte[] ImmagineProfilo { get; set; }
        public string Tipo { get; set; }
        public DateTime DataRegistrazione { get; set; }
        public string Abilitato { get; set; }
    }
}
