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
        public MVCAPPELLO? Appello { get; set; }
        public string? Link { get; set; }
        public string? Tipologia { get; set; }
        public Guid? Codice_Test_DA { get; set; }

        [ForeignKey("Codice_Test_DA")]
        [ValidateNever]
        public MVCTest_DA? Test_DA { get; set; }
        public Guid? Codice_Test_DC { get; set; }

        [ForeignKey("Codice_Test_DC")]
        [ValidateNever]
        public MVCTest_DC? Test_DC { get; set; }

    }
}
