using RentBikeUsers.Domain.Enums;
using System.Text.Json.Serialization;

namespace RentBikeUsers.Domain.Entities
{
    public class DriversLicense : BaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DriversLicenseType DriversLicenseType { get; set; }
        public Guid DeliverymanId { get; set; }
        [JsonIgnore]
        public DeliverymanUser DeliverymanUser { get; set; }
    }
}
