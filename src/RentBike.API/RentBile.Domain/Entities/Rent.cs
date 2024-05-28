using RentBikeUsers.Domain.Entities;
using System.Text.Json.Serialization;

namespace RentBike.Domain.Entities
{
    public class Rent : BaseEntity
    {
        public Guid DeliverymanUserId { get; set; }
        public Guid BikeId { get; set; }
        public Guid RentPlanId { get; set; }
        public DateTime StartDate { get; set; } =DateTime.UtcNow.AddDays(1);
        public DateTime? EndDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public DeliverymanUser DeliverymanUser { get; set; }
        [JsonIgnore]
        public Bike Bike { get; set; }
        [JsonIgnore]
        public RentPlan RentPlan { get; set; }
    }
}
