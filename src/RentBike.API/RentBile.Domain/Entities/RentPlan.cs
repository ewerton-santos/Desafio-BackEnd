using RentBikeUsers.Domain.Entities;

namespace RentBike.Domain.Entities
{
    public class RentPlan : BaseEntity
    {
        public int Days { get; set; }
        public double FinePercentage {  get; set; }
    }
}
