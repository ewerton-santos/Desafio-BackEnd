using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Application.Handlers.Commands
{
    public class CreateDeliverymanCommandHandler : IRequestHandler<CreateDeliverymanCommand>
    {
        private readonly ILogger<CreateDeliverymanCommandHandler> _logger;
        private readonly IDeliverymanUserRepository _deliverymanUserRepository;

        public CreateDeliverymanCommandHandler(ILogger<CreateDeliverymanCommandHandler> logger
            , IDeliverymanUserRepository deliverymanUserRepository)
        {
            _logger = logger;
            _deliverymanUserRepository = deliverymanUserRepository;
        }

        public async Task Handle(CreateDeliverymanCommand request, CancellationToken cancellationToken)
        {
            var deliveryman = new DeliverymanUser
            {
                Name = request.Name,
                Cnpj = request.Cnpj,
                BirthDate = request.Birthdate,
                DriversLicense = new DriversLicense
                {
                    Number = request.DriversLicenseNumber,
                    DriversLicenseType = request.DriversLicenseType
                }
            };
            await _deliverymanUserRepository.Add(deliveryman);
        }
    }
}
