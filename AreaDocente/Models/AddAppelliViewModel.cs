using AreaDocente.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models
{
    public class AddAppelliViewModel
    {
        public Guid? K_Esame { get; set; }
        public DateTime? DataAppello { get; set; }
        public DateTime? DataVerbalizzazione { get; set; }
        public string? Tipo { get; set; }
        public string? Link { get; set; }
        public DateTime? DataOrale { get; set; }
    }
}
