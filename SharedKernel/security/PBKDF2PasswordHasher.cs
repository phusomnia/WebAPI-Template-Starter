using Microsoft.AspNetCore.Identity;
using WebAPI_Template_Starter.Features.AuthAPI.Utils;
using WebAPI_Template_Starter.SharedKernel.configuration;

namespace WebAPI_Template_Starter.SharedKernel.security;

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