using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AreaPubblica.Models.Entities
{
    public class Corso
    {
        [Key]
        public Guid K_Corso { get; set; }
        public string TitoloCorso { get; set; }
        public Guid K_Facolta { get; set; }
        [ForeignKey("K_Facolta")]
        [ValidateNever]
        public Facolta Facolta { get; set; }
        public Guid K_TipoCorso { get; set; }
        [ForeignKey("K_TipoCorso")]
        [ValidateNever]
        public TipoCorso TipoCorso { get; set; }
        public string MinimoCFU { get; set; }
    }
}
