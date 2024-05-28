using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class DeleteBikeCommandHandler : IRequestHandler<DeleteBikeCommand>
    {
        readonly ILogger<DeleteBikeCommandHandler> _logger;
        readonly IAdminUserRepository _adminUserRepository;
        readonly IBikeRepository _bikeRepository;

        public DeleteBikeCommandHandler(ILogger<DeleteBikeCommandHandler> logger,
            IAdminUserRepository adminUserRepository, IBikeRepository bikeRepository)
        {
            _logger = logger;
            _adminUserRepository = adminUserRepository;
            _bikeRepository = bikeRepository;
        }
        public async Task Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetById(Guid.Parse(request.AdminUserId)) ?? throw new AdminUserNotFoundException();
            var bike = await _bikeRepository.GetById(request.Id) ?? throw new BikeNotFoundException();
            await _bikeRepository.Remove(bike);
        }
    }
}
