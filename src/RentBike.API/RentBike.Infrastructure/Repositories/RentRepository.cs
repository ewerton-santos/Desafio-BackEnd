using Microsoft.EntityFrameworkCore;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Infrastructure.Repositories
{
    public class RentRepository : Repository<Rent, Guid>, IRentRepository
    {
        public RentRepository(DataContext context) : base(context)
        {
        }
    }
}
