namespace RentBike.Application.Services.Interfaces
{
    public interface IRabbitMQListenerService
    {
        void Register();
        void Unregister();
    }
}
