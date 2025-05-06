namespace AreaStudente.Models
{
    public class StudenteDashboardViewModel
    {
        public ShowStudenteViewModel? Studente { get; set; }
        public List<ComunicazioneViewModel>? Comunicazioni { get; set; }
        public AddComunicazioneViewModel NuovaComunicazione { get; set; }
    }
}
