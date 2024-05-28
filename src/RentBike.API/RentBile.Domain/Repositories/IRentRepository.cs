using RentBike.Domain.Entities;

namespace RentBike.Domain.Repositories
{
    public interface IRentRepository : IRepository<Rent, Guid>
    {
    }
}
