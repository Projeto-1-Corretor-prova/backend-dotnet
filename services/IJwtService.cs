using backend.models;

namespace backend.services;

public interface IJwtService
{
    string GenerateToken(Teacher teacher);
    int? ValidateToken(string token);
}
