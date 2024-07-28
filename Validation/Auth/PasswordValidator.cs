using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.DTO.Auth;

namespace NotesApp.Validation.Auth
{
    public class PasswordValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            var registerDTO = (RegisterDTO)validationContext.ObjectInstance;
            if (registerDTO.Password != null)
            {
                if (registerDTO.Password.Length < 6)
                {
                    return new ValidationResult("Password must be at least 6 characters long");
                }
                if (registerDTO.Password.Any(char.IsDigit) == false)
                {
                    return new ValidationResult("Password must contain at least one digit");
                }

                if (registerDTO.Password.Any(char.IsUpper) == false)
                {
                    return new ValidationResult(
                        "Password must contain at least one uppercase letter"
                    );
                }

                if (registerDTO.Password.Any(char.IsLower) == false)
                {
                    return new ValidationResult(
                        "Password must contain at least one lowercase letter"
                    );
                }

                if (registerDTO.Password.Any(ch => !char.IsLetterOrDigit(ch)) == false)
                {
                    return new ValidationResult(
                        "Password must contain at least one special character"
                    );
                }

                return ValidationResult.Success;
            }
            return new ValidationResult("Password is required");
        }
    }
}
