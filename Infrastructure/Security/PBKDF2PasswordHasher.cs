using Microsoft.AspNetCore.Identity;
using WebAPI_Template_Starter.Infrastructure.Annotation;

namespace WebAPI_Template_Starter.Infrastructure.Security;

[Component]
public class PBKDF2PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<String> _passwordHasher = new();
    
    public string hashPassword(string username, string password)
    {
        return _passwordHasher.HashPassword(username, password);
    }

    public bool verifyPassword(string username, string providedPassword, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(username, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}