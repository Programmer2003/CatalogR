using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogR.CustomValidationAttributes
{
    public sealed class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) new ValidationResult(GetErrorMessage());

            IFormFile? file = value as IFormFile;
            if (file != null && file.Length > maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }


        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {maxFileSize} bytes.";
        }
    }
}