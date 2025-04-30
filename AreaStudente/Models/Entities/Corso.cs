using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaStudente.Models.Entities
{
    public class Corso
    {
        [Key]
        public Guid K_Corso { get; set; }
        public string? TitoloCorso { get; set; }
        public Guid? K_Facolta { get; set; }
        public Guid? K_TipoCorso { get; set; }
        public string? MinimoCFU { get; set; }
        [ForeignKey("K_Facolta")]
        [ValidateNever]

        public Facolta Facolta { get; set; }



    }
}
