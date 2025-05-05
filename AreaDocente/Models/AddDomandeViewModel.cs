namespace AreaDocente.Models
{
    public class AddDomandeViewModel
    {
        public int? Numero_Domanda { get; set; }
        public string? Domanda { get; set; }
        public string? Risposte { get; set; }
        public string? RispostaCorretta { get; set; }
        public string? RispostaData { get; set; }
        public Guid? K_Prova { get; set; }
    }
}
