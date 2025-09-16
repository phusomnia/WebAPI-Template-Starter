using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebAPI_Template_Starter.Infrastructure.RabbitMQ;

namespace WebAPI_Template_Starter.Pun.Infrastructure.RabbitMQ;

public class RabbitMQConsumer : BackgroundService
{
    private readonly RabbitMQConfig _config;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMQConsumer(
        RabbitMQConfig config
    )
    {
        _config = config;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _config.rabbitMQ().CreateConnectionAsync(cancellationToken: stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
        
        await _channel.QueueDeclareAsync(
            queue: "message-queue", 
            durable: false, 
            exclusive: false, 
            autoDelete: false,
            arguments: null, 
            cancellationToken: stoppingToken
        );
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"[Consumer] Received: {message}");
            
            await Task.Delay(500, stoppingToken);
            
            await _channel.BasicAckAsync(
                deliveryTag: ea.DeliveryTag,
                multiple: false, 
                cancellationToken: stoppingToken
            );
        };
        
        await _channel.BasicConsumeAsync(
            "message-queue", 
            autoAck: false, 
            consumer: consumer, 
            cancellationToken: stoppingToken
        );
        
        var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        stoppingToken.Register(() => tcs.TrySetResult());
        await tcs.Task;
        
        _channel.Dispose();
        _connection.Dispose();
    }
}