using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaStudente.Models
{
    public class ShowStudenteViewModel
    {
        [Key]
        public Guid K_Studente { get; set; }
        public string? Email { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string? Indirizzo { get; set; }
        public string? CAP { get; set; }
        public string? Citta { get; set; }
        public string? Provincia { get; set; }
        public byte[]? ImmagineProfilo { get; set; }
        public int? Matricola { get; set; }
        public DateTime? DataImmatricolazione { get; set; }
        public string? Abilitato { get; set; }

        [ForeignKey("K_Corso")]
        public Guid? K_Corso { get; set; }



    }
}
