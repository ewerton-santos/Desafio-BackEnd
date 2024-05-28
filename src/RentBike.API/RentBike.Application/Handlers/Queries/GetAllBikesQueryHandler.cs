using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Queries
{
    public class GetAllBikesQueryHandler : IRequestHandler<GetAllBikesQuery, IEnumerable<Bike>>
    {
        readonly ILogger<GetAllBikesQueryHandler> _logger;
        readonly IAdminUserRepository _adminUserRepository;
        readonly IBikeRepository _bikeRepository;

        public GetAllBikesQueryHandler(ILogger<GetAllBikesQueryHandler> logger,
            IAdminUserRepository adminUserRepository, IBikeRepository bikeRepository)
        {
            _logger = logger;
            _adminUserRepository = adminUserRepository;
            _bikeRepository = bikeRepository;
        }

        public async Task<IEnumerable<Bike>> Handle(GetAllBikesQuery request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetById(Guid.Parse(request.AdminUserId)) ?? throw new AdminUserNotFoundException("User isn't Admin");
            return await _bikeRepository.GetAll();
        }
    }
}
