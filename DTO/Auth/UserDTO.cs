using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.DTO.Auth
{
    public class UserDTO
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Token { get; set; }

        [Required]
        public required bool isVerified { get; set; }
    }
}
