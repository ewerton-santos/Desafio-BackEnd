using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Infrastructure.Repositories
{
    public class AdminUserRepository : Repository<AdminUser, Guid>, IAdminUserRepository
    {
        public AdminUserRepository(DataContext context) : base(context)
        {

        }
    }
}
