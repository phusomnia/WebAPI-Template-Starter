using System.Security.Cryptography;
using System.Text;
using WebAPI_Template_Starter.SharedKernel.configuration;

namespace WebAPI_Template_Starter.SharedKernel.security;

[Component]
public class OtpProvider
{
    private static readonly RNGCryptoServiceProvider Rng = new();
    
    public String generateNumericOtp(int length = 6)
    {
        var bytes = new byte[length];
        Rng.GetBytes(bytes);
        var digits = new StringBuilder(length);
        foreach (var b in bytes) digits.Append((b % 10).ToString());
        return digits.ToString();
    }

    public String hashOtp(String otp, String salt)
    {
        using var sha = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes(otp + salt);
        return Convert.ToBase64String(sha.ComputeHash(combined));
    }
}