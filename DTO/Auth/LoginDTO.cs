using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.Validation.Auth;

namespace NotesApp.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [LoginValidator(ErrorMessage = "Invalid email or password")]
        public required string Password { get; set; }
    }
}
