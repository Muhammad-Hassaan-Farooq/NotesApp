using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.Validation.Auth;

namespace NotesApp.DTO.Auth
{
    public class RegisterDTO
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required]
        [PasswordValidator]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public required string Password { get; set; }
    }
}
