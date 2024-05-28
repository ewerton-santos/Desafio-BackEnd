using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Enums;

namespace RentBike.Application.Handlers.Commands
{
    public class CreateRentBikeCommandHandler : IRequestHandler<CreateRentBikeCommand>
    {
        private readonly ILogger<CreateRentBikeCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IBikeRepository _bikeRepository;
        private readonly IDeliverymanUserRepository _deliverymanUserRepository;
        private readonly IRentRepository _rentRepository;
        private readonly IRentPlanRepository _rentPlanRepository;

        public CreateRentBikeCommandHandler(ILogger<CreateRentBikeCommandHandler> logger, IMediator mediator
            , IBikeRepository bikeRepository, IDeliverymanUserRepository deliverymanUserRepository, 
            IRentRepository rentRepository, IRentPlanRepository rentPlanRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _bikeRepository = bikeRepository;
            _deliverymanUserRepository = deliverymanUserRepository;
            _rentRepository = rentRepository;
            _rentPlanRepository = rentPlanRepository;
        }

        public async Task Handle(CreateRentBikeCommand request, CancellationToken cancellationToken)
        {
            var deliveryman = await _deliverymanUserRepository.GetById(request.DeliverymanUserId, p => p.DriversLicense) ?? throw new Exception("Deliveryman' User not found");
            if (deliveryman.DriversLicense == null) 
                throw new Exception("Driver's license not found");
            if (deliveryman.DriversLicense.DriversLicenseType == DriversLicenseType.B) 
                throw new Exception("Driver not qualified for the category");
            var rentplan = await _rentPlanRepository.GetById(request.RentPlanId) ?? throw new Exception("Rent Plan not found");
            var bikes = await _bikeRepository.Find(c => c.IsAvailable);
            if (bikes == null || !bikes.Any())
                throw new Exception("There are no motorbikes available");
            var bike = bikes.FirstOrDefault();
            await _rentRepository.Add(new Rent 
            {
                DeliverymanUserId = deliveryman.Id,
                RentPlanId = rentplan.Id,
                BikeId = bike.Id,
                ExpectedEndDate = DateTime.UtcNow.AddDays(rentplan.Days + 1)
            });
            bike.IsAvailable = false;
            await _bikeRepository.Update(bike);
        }
    }
}
