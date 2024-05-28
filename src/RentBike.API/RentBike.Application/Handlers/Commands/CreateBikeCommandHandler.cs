using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class CreateBikeCommandHandler : IRequestHandler<CreateBikeCommand>
    {
        private readonly ILogger _logger;
        private readonly IBikeRepository _bikeRepository;
        private readonly IAdminUserRepository _adminUserRepository;
        public CreateBikeCommandHandler(ILogger<CreateBikeCommand> logger, IBikeRepository bikeRepository, IAdminUserRepository adminUserRepository)
        {
            _logger = logger;
            _bikeRepository = bikeRepository;
            _adminUserRepository = adminUserRepository;
        }
        public async Task Handle(CreateBikeCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetById(Guid.Parse(request.AdminUserId));
            if (adminUser == null)
                throw new AdminUserNotFoundException();
            await _bikeRepository.Add(new Bike
            {
                Model = request.Model,
                Year = request.Year,
                Plate = request.Plate
            });
        }
    }
}
