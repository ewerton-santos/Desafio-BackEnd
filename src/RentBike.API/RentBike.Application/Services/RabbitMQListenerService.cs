using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentBike.Application.Commands;
using RentBike.Application.Services.Interfaces;
using RentBike.Domain;
using System.Text;
using System.Text.Json;

namespace RentBike.Application.Services
{
    public class RabbitMQListenerService : IRabbitMQListenerService, IDisposable
    {
        readonly ILogger _logger;
        readonly IConfig _config;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQListenerService(ILogger<RabbitMQListenerService> logger, IConfig config, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _config = config;
            _factory = new ConnectionFactory { HostName = config.RabbitMQHost(), UserName = config.RabbitMQUser(), Password = config.RabbitMQPassword() };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _serviceScopeFactory = serviceScopeFactory;
        }      

        public void Register()
        {
            _channel.QueueDeclare(queue: _config.RabbitMQQueueName(), durable: true, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var headers = args.BasicProperties.Headers;
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message received - {message}");
                CreateOrder(message);
            };
            _channel.BasicConsume(queue: _config.RabbitMQQueueName(), autoAck: true, consumer: consumer);            
        }

        public void CreateOrder(string message) 
        {
            var obj = JsonSerializer.Deserialize<Dictionary<string, double>>(message);
            obj.TryGetValue("DeliveryFee", out double deliveryFee);
            var command = new CreateOrderCommand { DeliveryFee = deliveryFee };
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            mediator.Send(command).Wait();
        }

        public void Unregister() => _connection.Close();
        public void Dispose() => Unregister();        
    }
}
