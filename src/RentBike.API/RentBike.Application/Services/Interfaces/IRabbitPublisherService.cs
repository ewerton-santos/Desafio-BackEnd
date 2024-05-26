namespace RentBike.Application.Services.Interfaces
{
    public interface IRabbitPublisherService
    {
        void PublishMessage(object message, Dictionary<string, object> headers = null);
    }
}
