using RentBike.Domain.Enums;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Domain.Entities
{
    public class Order : BaseEntity
    {
        public double DeliveryFee { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid? DeliverymanId { get; set; }
    }
}
