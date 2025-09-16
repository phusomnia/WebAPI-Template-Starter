using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace WebAPI_Template_Starter.Infrastructure.Pub_Sub;

public class RabbitMQPublisher : IPublisher
{
    private readonly RabbitMQConfig _config;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMQPublisher(
        RabbitMQConfig config
    )
    {
        _config = config;
    }

    public async Task publishMessageAsync<T>(string queueName, T message)
    {
        _connection = await _config.rabbitMQ().CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        var properties = new BasicProperties();
        properties.Persistent = true;
        
        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            false,
            basicProperties: properties,
            body: body
        );
        
        Console.WriteLine($"[Publisher] Sent: {JsonSerializer.Serialize(message)}");

        _channel.Dispose();
        _connection.Dispose();
    }
}