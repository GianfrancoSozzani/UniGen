using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models.Entites
{
    public class Materiali
    {
        [Key]
        public Guid K_Materiale { get; set; }
        public Guid K_Esame { get; set; }
        public string Titolo { get; set; }
        public byte[] Materiale { get; set; }
        public string Tipo { get; set; }
    }
}
