using RentBikeUsers.Domain.Enums;

namespace RentBikeUsers.Domain.Entities
{
    public class DriversLicence : BaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public DriversLicenceType DriversLicenceType { get; set; }
        public Guid DeliverymanId { get; set; }
        public DeliverymanUser DeliverymanUser { get; set; } = null!;
    }
}
