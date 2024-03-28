using System.ComponentModel.DataAnnotations;

namespace CatalogR.CustomValidationAttributes
{
    public sealed class PermittedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;
        public PermittedExtensionsAttribute(string[] permittedExtensions) => extensions = permittedExtensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(GetErrorMessage());

            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extensions.Contains(extension.ToLower()))
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage() => $"This image file extension is not allowed.";
    }
}