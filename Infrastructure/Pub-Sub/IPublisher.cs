namespace WebAPI_Template_Starter.Infrastructure.Pub_Sub;

public interface IPublisher
{
    Task publishMessageAsync<T>(string queueName, T message);
}