using System.ComponentModel.DataAnnotations;

namespace AreaPubblica.Models.Entities
{
    public class Operatore
    {
        [Key]
        public Guid K_Operatore { get; set; }
        public string USR { get; set; }
        public string PWD { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

    }
}
