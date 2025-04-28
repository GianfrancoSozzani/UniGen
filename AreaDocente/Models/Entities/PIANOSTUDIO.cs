using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class PIANOSTUDIO
    {
        [Key]
        public Guid K_PianoStudio { get; set; }
        public Guid K_Corso { get; set; }
        [ForeignKey("K_Corso")]
        public CORSO Corso { get; set; }
        public Guid K_Esame { get; set; }
        [ForeignKey("K_Esame")]
        public ESAME Esame { get; set; }
        /// <summary>
        /// inteso come anno accademico corrente (es. 2024/2025)
        /// </summary>
        public string AnnoAccademico { get; set; }
        /// <summary>
        /// è obbligatorio? (S) si (N) no
        /// </summary>
        public char Obbligatorio { get; set; }
    }
}
