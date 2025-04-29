using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AreaDocente.Models.Entities
{
    public class Materiali
    {
        [Key]
        public Guid K_Materiale { get; set; }
        public string Titolo { get; set; }
        public byte[] Materiale { get; set; }
        public string Tipo { get; set; }
        public Guid K_Esame { get; set; }
        [ForeignKey("K_Esame")]

        [ValidateNever]
        public MVCEsame esame{ get; set; }
    }
}
