using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCTest_DA
    {
        [Key]
        public Guid K_Test_DA { get; set; }
        public byte? Numero_Domanda { get; set; }
        public string? Domanda { get; set; }
        public string? Risposta { get; set; }
        public Guid? K_Prova { get; set; }

        [ForeignKey("K_Prova")]
        [ValidateNever]
        public MVCPROVA? Prova { get; set; }
    }
}
