using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Features.AccountAPI;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Infrastructure.Security.Jwt;

[Component]
public class JwtTokenProvider
{
    private readonly String _key;
    private readonly String _issuer;
    private readonly String _audience;
    private readonly String _atExpireTime; 
    private readonly String _rtExpireTime;

    private readonly AccountRepository _accountRepo;

    public JwtTokenProvider(
        IConfiguration config,
        AccountRepository accountRepo
    )
    {
        var atTime = new DataTable().Compute(config["Jwt:atExpiryInMillisecond"], null).ToString();

        _key          = config["Jwt:SecretKey"] ?? "";
        _issuer       = config["Jwt:Issuer"]    ?? "";
        _audience     = config["Jwt:Audience"]  ?? "";
        _atExpireTime = atTime ?? "";
        
        _accountRepo = accountRepo;
    }

    public async Task<String> generateAccessToken<TUser>(TUser user) where TUser : class
    {
        var dictUser = ConverterUtils.toDict(user);
        Console.WriteLine($"GenToken: {CustomJson.json(dictUser, CustomJsonOptions.WriteIndented)}");

        var permissions = (await _accountRepo.findAccountDetail(dictUser["username"].ToString())).Select(x => x["permissionList"]);
        var jsonPermission = JsonConvert.DeserializeObject<List<String>>(permissions.FirstOrDefault().ToString());
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, dictUser["id"].ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", dictUser["roleName"].ToString())
        };
        
        // Add permission
        foreach (var permission in jsonPermission) {
            claims.Add(new Claim("permission", permission));
        }
        
        string secretKey = _key;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var tokenDes = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMilliseconds(Convert.ToInt32(_atExpireTime)),
            signingCredentials: credentials
        );
        
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.WriteToken(tokenDes);
        
        return jwtToken;
    }

    public async Task<String> generateRefreshToken<TUser>(TUser user) where TUser : class
    {
        var u = await _accountRepo.findByUsername(getValue(user, "username")?.ToString()!);

        var dt = DateTime.UtcNow.AddMilliseconds(Convert.ToInt32(_rtExpireTime));
        
        RefreshToken rt = new RefreshToken();
        rt.Id = Guid.NewGuid().ToString();
        rt.Token = Guid.NewGuid().ToString();
        rt.AccountId = u?.Id;
        rt.ExpiryDate = TimeUtils.AsiaTimeZone(dt);
        
        // _refreshTokenRepository.add(rt);
        
        return rt.Token;
    }


    public JwtSecurityToken? extractAllClaims(String token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken;
    }

    public JwtPayload? extractPayload(string token)
    {
        return extractAllClaims(token)!.Payload;
    }

    public Object? getValue<TUser>(TUser obj, string propertyName) where TUser : class
    {
        var prop = typeof(TUser).GetProperties()
            .FirstOrDefault(p => 
                string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase)
            );
        return prop?.GetValue(obj) ?? "";
    }

    public ClaimsPrincipal? validateToken(String token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
            ClockSkew = TimeSpan.Zero
        }, out _);
    }
}