namespace RentBike.Application.Services.Interfaces
{
    public interface IRabbitMQListenerService
    {
        void Registar();
        void Unregister();
    }
}
