namespace AreaDocente.Models;
using Microsoft.AspNetCore.Http;
public class AddMaterialiViewModel
{
    public string Titolo { get; set; }
    public IFormFile materiale { get; set; }
    public string Tipo { get; set; }

}

