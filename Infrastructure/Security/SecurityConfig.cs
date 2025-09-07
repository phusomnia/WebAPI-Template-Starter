using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI_Template_Starter.Infrastructure.Security.Jwt;
using WebAPI_Template_Starter.Infrastructure.Security.permission;

namespace WebAPI_Template_Starter.Infrastructure.Security;

public class SecurityConfig
{
    public static void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        
        //
        // builder.Services.AddScoped<JwtFilter>();
        // builder.Services.AddControllers(options =>
        // {
        //     // Apply filter globally (all controllers/actions)
        //     options.Filters.AddService<JwtFilter>();
        // });
        
        // -- Config openapi security schema -- 
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
        
        // -- Config auth service -- 
        builder.Services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            String secretKey = builder.Configuration["Jwt:SecretKey"] ?? "";
            String issuer = builder.Configuration["Jwt:Issuer"] ?? "";
            String audience = builder.Configuration["Jwt:Audience"] ?? "";
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
    }
}