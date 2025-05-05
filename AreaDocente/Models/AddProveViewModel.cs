namespace AreaDocente.Models
{
    public class AddProveViewModel
    {
        public Guid K_Prova { get; set; }
        public Guid? K_Appello { get; set; }
        public string? Link { get; set; }
        public string? Tipologia { get; set; }
        public Guid? Codice_Test_DA { get; set; }
        public Guid? Codice_Test_DC { get; set; }
        public Guid? K_Esame { get; set; }
    }
}
