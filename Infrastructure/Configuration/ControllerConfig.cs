using System.Text.Json.Serialization;

namespace WebAPI_Template_Starter.Infrastructure.Configuration;


public static class ControllerConfig
{
    public static IServiceCollection ControllerConfigExtension(this IServiceCollection services)
    {        
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                policy => policy
                    .WithOrigins("http://localhost:1337") 
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        });

        services.AddControllers();

        return services;
    }
}