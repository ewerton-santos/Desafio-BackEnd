using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Queries
{
    public class GetBikeByPlateQueryHandler : IRequestHandler<GetBikeByPlateQuery, Bike>
    {
        readonly ILogger<GetBikeByPlateQueryHandler> _logger;
        readonly IBikeRepository _bikeRepository;
        readonly IAdminUserRepository _adminUserRepository;

        public GetBikeByPlateQueryHandler(ILogger<GetBikeByPlateQueryHandler> logger,
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
                ? throw new AdminUserNotFoundException()
                : (await _bikeRepository.Find(p => p.Plate == request.Plate)).FirstOrDefault();
        }
    }
}
