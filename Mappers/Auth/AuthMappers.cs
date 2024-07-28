using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.DTO.Auth;
using NotesApp.Models;

namespace NotesApp.Mappers.Auth
{
    public static class AuthMappers
    {
        public static UserDTO MapUserToUserDTO(User user, string token)
        {
            return new UserDTO
            {
                Username = user.Username,
                Email = user.Email,
                Token = token,
                isVerified = user.IsVerified
            };
        }
    }
}
