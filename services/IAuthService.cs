namespace backend.services;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
