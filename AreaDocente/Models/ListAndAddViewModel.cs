using Microsoft.AspNetCore.Mvc;

namespace AreaDocente.Models
{
    public class ListAndAddViewModel
    {
        public List<IGrouping<Guid, AreaDocente.Models.Entities.Comunicazione>>? Comunicazioni { get; set; }
        [BindProperty]
        public AddComunicazioneViewModel? AddComunicazione { get; set; }
    }
}
