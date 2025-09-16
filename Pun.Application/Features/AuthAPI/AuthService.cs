using System.Net;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using WebAPI_Template_Starter.Application.AccountAPI;
using WebAPI_Template_Starter.Application.Features.AccountAPI;
using WebAPI_Template_Starter.Application.Features.CacheAPI;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Domain.Enums;
using WebAPI_Template_Starter.Features.AuthAPI.Dtos;
using WebAPI_Template_Starter.Features.AuthAPI.Utils;
using WebAPI_Template_Starter.Features.CacheAPI;
using WebAPI_Template_Starter.Features.CacheAPI.Dtos;
using WebAPI_Template_Starter.Infrastructure.Configuration;
using WebAPI_Template_Starter.Infrastructure.CustomException;
using WebAPI_Template_Starter.Infrastructure.Security;
using WebAPI_Template_Starter.Infrastructure.Security.Jwt;

namespace WebAPI_Template_Starter.Features.AuthAPI;

[Service]
public class AuthService
{
    private readonly AccountRepository     _accountRepo;
    private readonly JwtTokenProvider      _tokenProvider;
    private readonly PBKDF2PasswordHasher  _passwordHasher;
    private readonly IFluentEmail          _fluentEmail;
    private readonly OtpProvider           _otpProvider;
    private readonly CacheService          _cacheService;

    public AuthService(
        AccountRepository    accountRepo,
        JwtTokenProvider     tokenProvider,
        PBKDF2PasswordHasher passwordHasher,
        IFluentEmail         fluentEmail,
        OtpProvider          otpProvider,
        CacheService         cacheService
    )
    {
        _accountRepo    = accountRepo;
        _tokenProvider  = tokenProvider;
        _passwordHasher = passwordHasher;
        _fluentEmail    = fluentEmail;
        _otpProvider    = otpProvider;
        _cacheService   = cacheService;
    }

    public async Task<Account> register(AccountDTO account)
    {
        Account acc  = new Account();
        acc.Id = Guid.NewGuid().ToString();
        acc.RoleId = "2";
        acc.Username = account.username;
        acc.Password = _passwordHasher.hashPassword(account.username, account.password);
        
        var affectedRows = await _accountRepo.addAsync(acc);
        if(affectedRows < 0) throw new ApplicationException("Failed to register account");
        
        return acc;
    }

    public async Task<Dictionary<String, Object>> authenticate(AccountDTO req)
    {
        var user = (await _accountRepo.findAccountDetail(req.username)).FirstOrDefault();

        if (user == null) throw APIException.BadRequest("Can't find username");

        Boolean checkPassword = _passwordHasher.verifyPassword(user["username"].ToString()!, req.password, user["password"].ToString()!);
        
        if(!checkPassword) throw APIException.BadRequest("Wrong password");
        
        String accessToken = await _tokenProvider.generateAccessToken(user);
        String refreshToken = await _tokenProvider.generateRefreshToken(user);
        
        return toToken(accessToken, refreshToken);
    }
    
    public async Task<SendResponse> sendEmail(EmailRequest req)
    {
        var sendProcess = await _fluentEmail
            .To(req.to)
            .Subject(req.subject)
            .Body(req.body)
            .SendAsync();
        
        if(!sendProcess.Successful) throw new Exception(sendProcess.ErrorMessages.ToString());

        return sendProcess;
    }
    
    public async Task generateOTP(String email)
    {
        var otp = _otpProvider.generateNumericOtp(6);
        var salt = Guid.NewGuid().ToString("N");
        var hash = _otpProvider.hashOtp(otp, salt);
        var key = $"otp:{email}";
        
        var payload = new Payload(
            hash: hash,
            salt: salt,
            DateTime.Now,
            triesLeft: 2
        );
        
        // set otp value into redis cache
        var req = new SetCacheRequest(
            key: key,
            value: payload
        );
        await _cacheService.createInstance(CacheProvider.Redis).setAsync(req);
        
        // Send otp to email
        var emailReq = new EmailRequest(
            to: email,
            subject: "OTP Verification",
            body: otp
        );
        await sendEmail(emailReq);
    }
    
    public async Task verifyOtp(VerifyOtpRequest req)
    {
        var key = $"otp:{req.email}";

        // get from cache
        var reqCache = new GetCacheRequest(key);
        var payload = await _cacheService.createInstance(CacheProvider.Redis).getAsync<Payload>(reqCache);
        
        // check tries that u can access
        if(payload.triesLeft <= 0) throw new Exception("Time");
        
        // 
        var hash = _otpProvider.hashOtp(req.otptCode, payload.salt);
        if (!hash.Equals(payload.hash))
        {
            payload.triesLeft--;
            var retry = new SetCacheRequest(
                key: key,
                value: payload
            );
            await _cacheService.createInstance(CacheProvider.Redis).setAsync(retry);
            throw APIException.BadRequest("Otp is expired");
        }
    }

    public Dictionary<String, Object> toToken(String accessToken, String refreshToken)
    {
        return new Dictionary<string, object>
        {
            ["access-token"]  = accessToken,
            ["refresh-token"] = refreshToken
        };
    }
}


class Payload
{

    public string hash { get; set; }

    public string salt { get; set; }

    public DateTime createdAt { get; set; }

    public int triesLeft { get; set; }

    public Payload(int triesLeft)
    {
        this.triesLeft = triesLeft;
    }

    [JsonConstructor]
    public Payload(string hash, string salt, DateTime createdAt, int triesLeft)
    {
        this.hash = hash;
        this.salt = salt;
        this.createdAt = createdAt;
        this.triesLeft = triesLeft;
    }
}