using StackExchange.Redis;

namespace WebAPI_Template_Starter.Infrastructure.Config;

public class IntegrationConfig
{
    public static void Configure(WebApplicationBuilder builder)
    {
        // -- Config email --
        
        // -- Config cache --
        builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("Redis")));
        
        // -- Config chat --
        builder.Services.AddSignalR();
    }
}