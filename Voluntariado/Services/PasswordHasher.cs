using System.Security.Cryptography;
using System.Text;

namespace Voluntariado.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }
    }
}

