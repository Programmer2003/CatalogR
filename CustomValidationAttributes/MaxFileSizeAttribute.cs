using System.ComponentModel.DataAnnotations;

namespace CatalogR.CustomValidationAttributes
{
    public sealed class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxSize;
        public MaxFileSizeAttribute(int maxSize) => this.maxSize = maxSize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > maxSize)
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public string GetErrorMessage() => $"Maximum allowed file size is {maxSize/1024.0/1024.0} MBytes.";
    }
}