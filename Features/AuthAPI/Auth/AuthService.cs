using Microsoft.AspNetCore.Identity;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Features.AccountAPI;
using WebAPI_Template_Starter.Infrastructure.Annotation;
using WebAPI_Template_Starter.Infrastructure.Security;
using WebAPI_Template_Starter.Infrastructure.Security.Jwt;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.AuthAPI.Auth;

[Service]
public class AuthService
{
    private readonly AccountRepository _accountRepo;
    private readonly JwtTokenProvider _tokenProvider;
    private PBKDF2PasswordHasher _passwordHasher;

    public AuthService(
        AccountRepository accountRepo,
        JwtTokenProvider tokenProvider,
        PBKDF2PasswordHasher passwordHasher
    )
    {
        _accountRepo = accountRepo;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<Account> register(AccountDTO account)
    {
        Account acc = new Account();
        acc.Id = Guid.NewGuid().ToString();
        acc.RoleId = "2";
        acc.Username = account.username;
        acc.Password = _passwordHasher.hashPassword(account.username, account.password);
        
        var affectedRows = await _accountRepo.addAsync(acc);
        if(affectedRows < 0) throw new ApplicationException("Failed to register account");
        
        return acc;
    }

    public async Task<Dictionary<String, Object>> login(AccountDTO req)
    {
        var user = (await _accountRepo.findAccountDetail(req.username)).First() ?? throw new ApplicationException("Can't find username");
        Console.WriteLine(CustomJson.json(user, CustomJsonOptions.WriteIndented));
        Boolean checkPassword = _passwordHasher.verifyPassword(user["username"].ToString(), req.password, user["password"].ToString());
        
        if(!checkPassword) throw new ApplicationException("Invalid password");
        
        String accessToken = await _tokenProvider.generateAccessToken(user);
        // String refreshToken = _tokenProvider.generateRefreshToken(user);
        
        return await toDict(accessToken, "refreshToken");
    }
    
    public async Task<Dictionary<String, Object>> toDict(String accessToken, String refreshToken)
    {
        return new Dictionary<string, object>
        {
            ["access-token"] = accessToken,
            ["refresh-token"] = refreshToken
        };
    }
}