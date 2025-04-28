using System.ComponentModel.DataAnnotations;

namespace AreaStudente.Models
{
    public class ModificaStudenteViewModel
    {
        [Key]
        public Guid K_Studente { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string? Indirizzo { get; set; }
        public string? CAP { get; set; }
        public string? Citta { get; set; }
        public string? Provincia { get; set; }
        //ogni volta che carichiamo una immagine dobbiamo avere il tipo di immagine
        public byte[]? ImmagineProfilo { get; set; }
        // nuovo campo per il file caricato
        public IFormFile? ImmagineProfiloFile { get; set; }
        public string? Tipo { get; set; }

    }
}
