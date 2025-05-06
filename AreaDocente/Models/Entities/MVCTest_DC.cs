using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCTest_DC
    {
        [Key]
        public Guid K_Test_DC { get; set; }
        public byte? Numero_Domanda { get; set; }
        public string? Domanda { get; set; }
        public string? Risposte { get; set; }
        public Guid? K_Prova { get; set; }

        [ForeignKey("K_Prova")]
        [ValidateNever]
        public MVCPROVA? Prova { get; set; }
    }
}
