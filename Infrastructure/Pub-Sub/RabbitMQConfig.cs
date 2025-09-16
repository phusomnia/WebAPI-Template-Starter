using RabbitMQ.Client;
using WebAPI_Template_Starter.Infrastructure.Configuration;

namespace WebAPI_Template_Starter.Infrastructure.Pub_Sub;

[Configuration]
public class RabbitMQConfig
{
    private readonly ConnectionFactory _config;
    
    public RabbitMQConfig()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        _config = new ConnectionFactory
        {
            HostName = config["RabbitMq:host"] ?? "localhost",
            Port = 5672,
            UserName = config["RabbitMq:user"] ?? "guest",
            Password = config["RabbitMq:pass"] ?? "guest",
        };
    }

    public ConnectionFactory rabbitMQ() => _config;
}