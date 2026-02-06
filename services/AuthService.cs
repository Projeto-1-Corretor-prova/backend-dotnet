using System.Security.Cryptography;
using System.Text;

namespace backend.services;

public class AuthService : IAuthService
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hashedPassword);
    }
}
