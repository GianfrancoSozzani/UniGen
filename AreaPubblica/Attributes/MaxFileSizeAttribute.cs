using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AreaPubblica.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxFileSizeAttribute(int maxSize) => _maxSize = maxSize;

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            if (file != null && file.Length > _maxSize)
                return new ValidationResult($"La dimensione massima consentita è {_maxSize / (1024 * 1024)} MB.");
            return ValidationResult.Success!;
        }
    }
}