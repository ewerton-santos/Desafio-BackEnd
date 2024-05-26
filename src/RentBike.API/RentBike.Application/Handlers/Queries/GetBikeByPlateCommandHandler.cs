using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Queries
{
    public class GetBikeByPlateCommandHandler : IRequestHandler<GetBikeByPlateQuery, Bike>
    {
        readonly ILogger<GetBikeByPlateCommandHandler> _logger;
        readonly IBikeRepository _bikeRepository;
        readonly IAdminUserRepository _adminUserRepository;

        public GetBikeByPlateCommandHandler(ILogger<GetBikeByPlateCommandHandler> logger,
            IBikeRepository bikeRepository, IAdminUserRepository adminUserRepository)
        {
            _logger = logger;
            _bikeRepository = bikeRepository;
            _adminUserRepository = adminUserRepository;
        }

        public async Task<Bike> Handle(GetBikeByPlateQuery request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetById(Guid.Parse(request.AdminUserId));
            return adminUser == null
                ? throw new Exception("User isn't Admin")
                : (await _bikeRepository.Find(p => p.Plate == request.Plate)).FirstOrDefault();
        }
    }
}
