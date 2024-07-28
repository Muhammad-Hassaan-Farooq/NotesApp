using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.Data;
using NotesApp.DTO.Auth;
using NotesApp.Models;

namespace NotesApp.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(RegisterDTO registerDTO);
        Task<bool> VerifyUser(string email);
        Task<bool> DeleteUser(string email);
        Task<List<User>> GetUsers();
        Task<bool> UserExists(string email);
    }
}
