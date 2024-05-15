using System.ComponentModel.DataAnnotations;

namespace EFCore.Models.Common;

public class ValidDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string strValue && DateOnly.TryParse(strValue, out _))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid date format, Correct format: MM/dd/yyyy");
    }
}