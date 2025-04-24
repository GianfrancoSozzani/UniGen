using System.ComponentModel.DataAnnotations;

namespace AreaStudente.Models
{
    public class ModificaStudenteViewModel
    {
        
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string? Indirizzo { get; set; }
        public string? CAP { get; set; }
        public string? Citta { get; set; }
        public string? Provincia { get; set; }

        
       
       
    }
}
