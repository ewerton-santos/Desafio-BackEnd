using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class UpdateBikePlateCommandHandler : IRequestHandler<UpdateBikePlateCommand>
    {
        readonly ILogger<UpdateBikePlateCommandHandler> _logger;
        readonly IBikeRepository _bikeRepository;
        readonly IAdminUserRepository _adminUserRepository;

        public UpdateBikePlateCommandHandler(ILogger<UpdateBikePlateCommandHandler> logger
            , IBikeRepository bikeRepository, IAdminUserRepository adminUserRepository)
        {
            _logger = logger;
            _bikeRepository = bikeRepository;
            _adminUserRepository = adminUserRepository;
        }

        public async Task Handle(UpdateBikePlateCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetById(Guid.Parse(request.AdminUserId)) ?? throw new AdminUserNotFoundException();
            var bike = (await _bikeRepository.Find(p => p.Plate == request.Plate)).FirstOrDefault() ?? throw new BikeNotFoundException();
            bike.Plate = request.NewPlate;
            bike.LastUpdated = DateTime.UtcNow;
            await _bikeRepository.Update(bike);
        }
    }
}
