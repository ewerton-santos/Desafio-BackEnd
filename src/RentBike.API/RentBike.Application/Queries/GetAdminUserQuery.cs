using MediatR;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Application.Queries
{
    public class GetAdminUserQuery : IRequest<AdminUser>
    {
        public Guid Id { get; set; }
    }
}
