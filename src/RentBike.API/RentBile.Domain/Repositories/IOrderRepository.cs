using RentBike.Domain.Entities;

namespace RentBike.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
