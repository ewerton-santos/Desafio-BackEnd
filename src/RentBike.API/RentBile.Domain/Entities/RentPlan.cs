using RentBikeUsers.Domain.Entities;
using System.Text.Json.Serialization;

namespace RentBike.Domain.Entities
{
    public class RentPlan : BaseEntity
    {
        public int Days { get; set; }
        public int CostPerDay { get; set; }
        public double FinePercentage {  get; set; }
        [JsonIgnore]
        public ICollection<Rent> Rents { get; set; }
    }
}
