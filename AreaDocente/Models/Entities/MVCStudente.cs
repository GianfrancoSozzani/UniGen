using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models.Entities
{
    public class MVCStudente
    {
        [Key]
        public Guid K_Studente { get; set; }
        public string? Email { get; set; }
        public string? PWD { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string? Indirizzo { get; set; }
        public string? CAP { get; set; }
        public string? Citta { get; set; }
        public string? Provincia { get; set; }
        public byte[]? ImmagineProfilo { get; set; }
        public string? Tipo { get; set; }
        public DateTime? DataImmatricolazione { get; set; }
        public Guid? K_Corso { get; set; }
        public char? Abilitato { get; set; }
        public int? Matricola { get; set; }
    }
}
