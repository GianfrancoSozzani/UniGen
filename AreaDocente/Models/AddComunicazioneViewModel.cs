namespace AreaDocente.Models
{
    public class AddComunicazioneViewModel
    {
        public DateTime DataOraComunicazione { get; set; }
        public string? Soggetto { get; set; }
        public string? Testo { get; set; }
        public Guid? K_Studente { get; set; }
        public Guid? K_Docente { get; set; }
        public Guid? K_Esame { get; set; }
    }
}
