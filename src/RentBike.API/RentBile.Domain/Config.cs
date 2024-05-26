using Microsoft.Extensions.Configuration;

namespace RentBike.Domain
{
    public class Config : IConfig
    {
        readonly IConfiguration _configuration;
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string RabbitMQHost() => _configuration.GetSection("AppParameters:RabbitMQHost").Value ?? string.Empty;
        public string RabbitMQUser() => _configuration.GetSection("AppParameters:RabbitMQUser").Value ?? string.Empty;
        public string RabbitMQPassword() => _configuration.GetSection("AppParameters:RabbitMQPassword").Value ?? string.Empty;
        public string RabbitMQQueueName() => _configuration.GetSection("AppParameters:RabbitMQQueueName").Value ?? string.Empty;
    }
}
