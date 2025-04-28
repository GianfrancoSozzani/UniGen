using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class Appello
    {
        [Key]
        public Guid K_Appello { get; set; }
        public Guid K_Esame { get; set; }
        [ForeignKey("K_Esame")]
        [ValidateNever]
        public DateTime DataAppello { get; set; }
        public DateTime DataVerbalizzazione { get; set; }
        public char Tipo { get; set; }
        public string Link { get; set; }
        public DateTime DataOrale { get; set; }
        public Esame Esame { get; set; }
    }
}
