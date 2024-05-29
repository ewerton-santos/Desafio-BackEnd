using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }
    }
}
