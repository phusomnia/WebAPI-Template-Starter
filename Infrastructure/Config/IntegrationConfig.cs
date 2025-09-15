using System.Net;
using System.Net.Mail;
using System.Security.AccessControl;
using StackExchange.Redis;
using WebAPI_Template_Starter.Infrastructure.Pub_Sub;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Infrastructure.Config;

public static class IntegrationConfig
{
    public static IServiceCollection IntegrationConfigExtension(this IServiceCollection services, IConfiguration config)
    {
        
        // -- Config email --
        services
            .AddFluentEmail(config["Smtp:from"])
            .AddSmtpSender(new SmtpClient(config["Smtp:host"])
            {
                Port = Convert.ToInt32(config["Smtp:port"]),
                Credentials = new NetworkCredential(config["Smtp:user"], config["Smtp:pass"]),
                EnableSsl = true
            });
        
        // -- Config cache --
        var configCache = config.GetValue<String>("Redis") ?? "";
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configCache));
        
        // -- Config chat --
        services.AddSignalR();
        
        // -- pub-sub --
        // services.AddControllers();
        // services.AddSingleton<RabbitMQConfig>();
        // services.AddSingleton<IPublisher, RabbitMQPublisher>();
        // services.AddHostedService<RabbitMQConsumer>();
        
        return services;
    }
}
