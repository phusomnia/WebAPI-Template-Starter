namespace WebAPI_Template_Starter.Infrastructure.RabbitMQ;

public interface IPublisher
{
    Task publishMessageAsync<T>(string queueName, T message);
}