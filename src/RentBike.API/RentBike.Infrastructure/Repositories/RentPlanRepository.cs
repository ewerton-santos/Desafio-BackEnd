using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Infrastructure.Repositories
{
    public class RentPlanRepository : Repository<RentPlan, Guid>, IRentPlanRepository
    {
        public RentPlanRepository(DataContext context) : base(context)
        {
        }
    }
}
