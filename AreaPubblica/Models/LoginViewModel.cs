using System.ComponentModel.DataAnnotations;
using AreaPubblica.Data;
using AreaPubblica.Models.Entities;
namespace AreaPubblica.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato email non valido")]
        [Display(Name = "Email")] 
        public string? username { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria")]
        [MinLength(5, ErrorMessage = "La password deve contenere almeno 5 caratteri")]
        [DataType(DataType.Password)]
        public string? PWD { get; set; }
    }
}
