using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDocente.Models.Entities
{
    public class PROVA
    {
        [Key]
        public Guid K_Prova { get; set; }
        public Guid K_Appello { get; set; }
        [ForeignKey("K_Appello")]
        public APPELLO Appello { get; set; }
        public string Link { get; set; }
        /// <summary>
        /// Domanda a risposta aperta o chiusa
        /// </summary>
        public string Tipologia { get; set; }

    }
}
