using NotesApp.Models;

namespace NotesApp.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
