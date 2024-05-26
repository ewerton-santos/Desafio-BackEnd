namespace RentBike.Domain
{
    public interface IConfig
    {
        string RabbitMQHost();
        string RabbitMQUser();
        string RabbitMQPassword();
        string RabbitMQQueueName();
    }
}
