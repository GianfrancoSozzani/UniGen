using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models.Entities
{
    public class MVCTest_DC
    {
        [Key]
        public Guid K_Test_DC { get; set; }
        public int? Numero_Domanda { get; set; }
        public string? Domanda { get; set; }
        public string? Risposte { get; set; }
        public string? RispostaCorretta { get; set; }
        public string? RispostaData { get; set; }
        public Guid? Codice_Test { get; set; }
    }
}
