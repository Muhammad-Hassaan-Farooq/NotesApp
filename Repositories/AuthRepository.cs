using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NotesApp.Data;
using NotesApp.DTO.Auth;
using NotesApp.Interfaces.Auth;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMongoCollection<User> _users;

        public AuthRepository(MongoDBContext context)
        {
            _context = context;
            _users = _context.Database.GetCollection<User>("Users");
        }

        public async Task<User> CreateUser(RegisterDTO registerDTO)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            User user = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = hashedPassword,
            };

            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user != null;
        }

        Task<bool> IAuthRepository.DeleteUser(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        Task<bool> IAuthRepository.VerifyUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
