using Microsoft.Extensions.Hosting;
using RentBike.Application.Services.Interfaces;

namespace RentBike.Application.Services
{
    public class RabbitMQListenerHostedService : BackgroundService, IHostedService
    {
        readonly IRabbitMQListenerService _listenerService;

        public RabbitMQListenerHostedService(IRabbitMQListenerService listenerService)
        {
            _listenerService = listenerService;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => _listenerService.Register(), stoppingToken);
        }
    }
}
