using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class MVCLibretto
    {
        [Key]
        public Guid K_Libretto { get; set; }
        public Guid? K_Studente { get; set; }

        [ForeignKey("K_Studente")]
        [ValidateNever]
        public MVCStudente? Studente { get; set; }
        public short? VotoEsame { get; set; }
        public char? Esito { get; set; }
        public Guid? K_Appello { get; set; }

        [ForeignKey("K_Appello")]
        [ValidateNever]
        public MVCAPPELLO? Appello { get; set; }
    }
}
