using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.DTO.Auth;

namespace NotesApp.Validation.Auth
{
    public class LoginValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            LoginDTO loginDTO = (LoginDTO)validationContext.ObjectInstance;
            if (loginDTO.Password != null)
            {
                if (loginDTO.Password.Length < 6)
                {
                    return new ValidationResult("Invalid email or password");
                }
                if (loginDTO.Password.Any(char.IsDigit) == false)
                {
                    return new ValidationResult("Invalid email or password");
                }

                if (loginDTO.Password.Any(char.IsUpper) == false)
                {
                    return new ValidationResult("Invalid email or password");
                }

                if (loginDTO.Password.Any(char.IsLower) == false)
                {
                    return new ValidationResult("Invalid email or password");
                }

                if (loginDTO.Password.Any(ch => !char.IsLetterOrDigit(ch)) == false)
                {
                    return new ValidationResult("Invalid email or password");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Password is required");
        }
    }
}
