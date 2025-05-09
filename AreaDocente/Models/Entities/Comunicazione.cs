using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class Comunicazione
    {
        [Key]
        public Guid K_Comunicazione { get; set; }
        public Guid Codice_Comunicazione { get; set; }
        public DateTime DataOraComunicazione { get; set; }
        public Guid? K_Soggetto { get; set; }
        public string? Soggetto { get; set; }
        public string? Testo { get; set; }
        public Guid? K_Studente { get; set; }
        [ForeignKey("K_Studente")]
        [ValidateNever]
        public MVCStudente? Studente { get; set; }
        public Guid? K_Docente { get; set; }
        [ForeignKey("K_Docente")]
        [ValidateNever]
        public MVCDOCENTE? Docente { get; set; }


        // Proprietà per il mittente (non mappate al DB come relazioni)
        [NotMapped]
        public MVCStudente? MittenteStudente { get; set; }
        [NotMapped]
        public MVCDOCENTE? MittenteDocente { get; set; }

        // Proprietà per il destinatario (non mappate al DB come relazioni)
        [NotMapped]
        public MVCStudente? DestinatarioStudente { get; set; }
        [NotMapped]
        public MVCDOCENTE? DestinatarioDocente { get; set; }

    }
}
