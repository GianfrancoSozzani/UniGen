using System.ComponentModel.DataAnnotations;

namespace AreaPubblica.Models.Entities
{
    public class TipoCorso
    {
        [Key]
        public Guid K_TipoCorso { get; set; }
        public string Tipo { get; set; }
        public string Durata { get; set; }
    }
}
