using RentBikeUsers.Domain.Entities;

namespace RentBike.Domain.Entities
{
    public class NotifyOrder : BaseEntity
    {
        public Guid OrderId {  get; set; }
        public Guid DeliverymanId { get; set; }
        //public Order Order { get; set; } = null;
        //public DeliverymanUser DeliverymanUser { get; set; } = null;
    }
}
