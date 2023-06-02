using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AspnetApp.Service.Attributes
{
    public class FormFileAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile formFile)
            {
                string[] extensions = new string[] {  ".mp4",".jpg" };
                string extension = Path.GetExtension(formFile.FileName);

                if (!extensions.Contains(extension))
                    return new ValidationResult("This file isn't allowed");
            }

            return ValidationResult.Success;
        }
    }
}
