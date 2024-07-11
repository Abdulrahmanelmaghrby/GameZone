using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Attribute
{
    public class MaxSizeAttribute :ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxSizeAttribute(int maxFileSize) 
        { 
            _maxSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {

                if (file.Length > _maxSize)
                {
                    return new ValidationResult("Max allowed Size is 1MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
