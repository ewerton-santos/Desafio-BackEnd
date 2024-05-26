using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class UpdaetBikePlateCommandHandler : IRequestHandler<UpdateBikePlateCommand>
    {
        readonly ILogger<UpdaetBikePlateCommandHandler> _logger;
        readonly IBikeRepository _bikeRepository;
        readonly IAdminUserRepository _adminUserRepository;

        public UpdaetBikePlateCommandHandler(ILogger<UpdaetBikePlateCommandHandler> logger
            , IBikeRepository bikeRepository, IAdminUserRepository adminUserRepository)
        {
            _logger = logger;
            _bikeRepository = bikeRepository;
            _adminUserRepository = adminUserRepository;
        }

        public async Task Handle(UpdateBikePlateCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetById(Guid.Parse(request.AdminUserId)) ?? throw new Exception("User isn`t Admin");
            var bike = (await _bikeRepository.Find(p => p.Plate == request.Plate)).FirstOrDefault() ?? throw new Exception("This bike not found");
            bike.Plate = request.NewPlate;
            bike.LastUpdated = DateTime.UtcNow;
            await _bikeRepository.Update(bike);
        }
    }
}
