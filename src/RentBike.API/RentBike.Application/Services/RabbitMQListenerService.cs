using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentBike.Application.Services.Interfaces;
using RentBike.Domain;
using System.Text;

namespace RentBike.Application.Services
{
    public class RabbitMQListenerService : IRabbitMQListenerService, IDisposable
    {
        readonly ILogger _logger;
        readonly IConfig _config;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQListenerService(ILogger<RabbitMQListenerService> logger, IConfig config)
        {
            _logger = logger;
            _config = config;
            _factory = new ConnectionFactory { HostName = config.RabbitMQHost(), UserName = config.RabbitMQUser(), Password = config.RabbitMQPassword() };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }        

        public void Registar()
        {
            _channel.QueueDeclare(queue: _config.RabbitMQQueueName(), durable: true, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var headers = args.BasicProperties.Headers;
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message received - {message}");
            };
            _channel.BasicConsume(queue: _config.RabbitMQQueueName(), autoAck: false, consumer: consumer);            
        }

        public void Unregister() => _connection.Close();
        public void Dispose() => Unregister();        
    }
}
