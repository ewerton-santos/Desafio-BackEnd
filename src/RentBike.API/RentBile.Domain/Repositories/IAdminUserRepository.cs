using RentBikeUsers.Domain.Entities;

namespace RentBike.Domain.Repositories
{
    public interface IAdminUserRepository : IRepository<AdminUser, Guid>
    {
    }
}
