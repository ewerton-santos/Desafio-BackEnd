﻿using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using RentBikeUsers.Domain.Enums;

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
                DriversLicence = new DriversLicence
                {
                    Number = request.DriversLicenseNumber,
                    DriversLicenceType = DriversLicenceType.A
                }
            };
            await _deliverymanUserRepository.Add(deliveryman);
        }
    }
}
