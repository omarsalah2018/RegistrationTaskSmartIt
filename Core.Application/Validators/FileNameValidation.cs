using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.Validators
{
    public class FileNameValidation : ValidationAttribute
    {
        private readonly string _fileName;
        public FileNameValidation(string fileName)
        {
            _fileName = fileName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {

            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }
}
