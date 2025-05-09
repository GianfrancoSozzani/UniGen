using System.ComponentModel.DataAnnotations;

namespace AreaDocente.Models
{
    public class EditMaterialiViewModel
    {
        [Key]
        public Guid K_Materiale { get; set; }
        public string? Titolo { get; set; }
        public byte[]? Materiale { get; set; }
        public IFormFile? MaterialeDA { get; set; }
        public string? Tipo { get; set; }
        public Guid? K_Esame { get; set; }
    }
}
