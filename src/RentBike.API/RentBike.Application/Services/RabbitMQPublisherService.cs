using RabbitMQ.Client;
using RentBike.Application.Services.Interfaces;
using RentBike.Domain;
using System.Text;
using System.Text.Json;

namespace RentBike.Application.Services
{
    public class RabbitMQPublisherService : IRabbitPublisherService
    {        
        private readonly string _host;
        private readonly string _user;
        private readonly string _password;
        private readonly string _queue;

        public RabbitMQPublisherService(IConfig config)
        {
            _host = config.RabbitMQHost();
            _user = config.RabbitMQUser();
            _password = config.RabbitMQPassword();
            _queue = config.RabbitMQQueueName();
        }

        public void PublishMessage(object message, Dictionary<string, object> headers = null)
        {
            var json = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(json);
            Publish(bytes, headers);
        }

        private void Publish(byte[] message, Dictionary<string, object> headers = null)
        {
            var factory = new ConnectionFactory { HostName = _host, UserName = _user, Password = _password };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;
            channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false, arguments: null);            
            channel.BasicPublish(exchange: "", routingKey: _queue, basicProperties: properties, body: message);
        }
    }
}
