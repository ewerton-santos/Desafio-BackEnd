using MediatR;
using RentBike.Application.Queries;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Application.Handlers.Queries
{
    public class GetAdminUserQueryHandler : IRequestHandler<GetAdminUserQuery, AdminUser>
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public GetAdminUserQueryHandler(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public async Task<AdminUser> Handle(GetAdminUserQuery request, CancellationToken cancellationToken)
        {
            return await _adminUserRepository.GetById(request.Id);
        }
    }
}
