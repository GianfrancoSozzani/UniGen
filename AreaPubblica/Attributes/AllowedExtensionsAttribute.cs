using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AreaPubblica.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions) => _extensions = extensions;

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(extension))
                    return new ValidationResult("Estensione del file non valida.");
            }
            return ValidationResult.Success!;
        }
    }
}
