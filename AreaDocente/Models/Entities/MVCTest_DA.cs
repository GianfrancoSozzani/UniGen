using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models.Entities
{
    public class MVCTest_DA
    {
        [Key]
        public Guid K_Test_DA { get; set; }
        public int Numero_Domanda { get; set; }
        public string Domanda { get; set; }
        public string Risposta { get; set; }
        public Guid Codice_Test { get; set; }
    }
}
