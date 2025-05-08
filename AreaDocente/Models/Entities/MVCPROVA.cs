using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCPROVA
    {
        [Key]
        public Guid K_Prova { get; set; }
        public Guid? K_Appello { get; set; }

        [ForeignKey("K_Appello")]
        [ValidateNever]
        public MVCAPPELLO? Appello { get; set; }
        public string? Link { get; set; }
        public string? Tipologia { get; set; }

        [ValidateNever]
        [NotMapped]
        public List<MVCTest_DA>? DomandeAperte { get; set; }

        [ValidateNever]
        [NotMapped]
        public List<MVCTest_DC>? DomandeChiuse { get; set; }
    }
}