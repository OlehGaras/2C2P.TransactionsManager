using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using _2C2P.TransactionsManager.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace _2C2P.TransactionsManager.Web.Attributes
{
    public class AllowedFileExtensionAttribute : ValidationAttribute
    {
        private readonly FileExtension[] _allowedExtensions;

        public AllowedFileExtensionAttribute(FileExtension[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileExtension = file.FileName.Split('.').Last();
                if (!Enum.TryParse(typeof(FileExtension), fileExtension, true, out object extension) ||
                    !_allowedExtensions.Contains((FileExtension)extension))
                {
                    return new ValidationResult($"{fileExtension} extension is not supported");
                }
            }

            return ValidationResult.Success;
        }
    }
}