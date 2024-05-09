using System.ComponentModel.DataAnnotations;
using API.Models.Enum;

namespace API.Models.Validation;

public abstract class CustomValidation
{
    public sealed class CheckGenderAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Gender is required.");
            }

            GenderType gender;
            if (value is GenderType)
            {
                gender = (GenderType)value;
            }
            else if (value is string genderString)
            {
                if (System.Enum.TryParse(genderString, true, out gender))
                {
                    return new ValidationResult("Invalid gender value.");
                }
            }
            else
            {
                return new ValidationResult("Invalid gender value.");
            }

            if (gender != GenderType.Unknown && gender != GenderType.Male && gender != GenderType.Female && gender != GenderType.Other)
            {
                return new ValidationResult("Gender must be Unknown, Male, Female, or Other.");
            }

            return ValidationResult.Success;
        }
    }
}