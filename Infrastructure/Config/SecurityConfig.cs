using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using WebAPI_Template_Starter.Infrastructure.Security.Authorization;
using WebAPI_Template_Starter.Infrastructure.Security.Jwt;

namespace WebAPI_Template_Starter.Infrastructure.Config;

public static class SecurityConfig
{
    public static IServiceCollection SecurityConfigExtension(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        
        // -- Config authorization --
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        
        // -- Config openapi security schema -- 
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
        
        // -- Config auth service -- 
        services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            String secretKey = config["Jwt:SecretKey"] ?? "";
            String issuer = config["Jwt:Issuer"] ?? "";
            String audience = config["Jwt:Audience"] ?? "";
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();
        
        return services;
    }
}