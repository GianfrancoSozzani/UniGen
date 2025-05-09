using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;


namespace AreaStudente.Models.Entities
{
    public class Pagamento
    {
        [Key]
        public Guid K_Pagamento { get; set; }   
        public Guid K_Studente { get; set; }
        [ForeignKey("K_Studente")]
        [ValidateNever]

        public Studente? Studente { get; set; }
        public string? Anno { get; set; }
        public DateTime? DataPagamento { get; set; }

        public decimal? Importo { get; set; }

        public string? Stato { get; set; }


    }
}
