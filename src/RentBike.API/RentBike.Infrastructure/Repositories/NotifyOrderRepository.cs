using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Infrastructure.Repositories
{
    public class NotifyOrderRepository : Repository<NotifyOrder, Guid>, INotifyOrderRepository
    {
        public NotifyOrderRepository(DataContext context) : base(context)
        {
        }
    }
}
