using Microsoft.EntityFrameworkCore;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Infrastructure.Repositories
{
    public class BikeRepository : Repository<Bike, Guid>, IBikeRepository
    {
        public BikeRepository(DataContext context) : base(context)
        {
        }
    }
}
