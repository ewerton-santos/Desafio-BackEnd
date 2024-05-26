using RentBike.Domain.Entities;

namespace RentBike.Domain.Repositories
{
    public interface IBikeRepository : IRepository<Bike, Guid>
    {
    }
}
