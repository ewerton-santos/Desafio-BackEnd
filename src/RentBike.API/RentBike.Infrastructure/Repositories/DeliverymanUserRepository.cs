using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Infrastructure.Repositories
{
    public class DeliverymanUserRepository : Repository<DeliverymanUser, Guid>, IDeliverymanUserRepository
    {
        public DeliverymanUserRepository(DataContext context) : base(context) { }
    }
}
