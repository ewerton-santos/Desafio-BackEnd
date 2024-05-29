using RentBike.Domain.Enums;
using RentBikeUsers.Domain.Entities;
using System.Text.Json.Serialization;

namespace RentBike.Domain.Entities
{
    public class Order : BaseEntity
    {
        public double DeliveryFee { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid? DeliverymanId { get; set; }
        [JsonIgnore]
        public ICollection<NotifyOrder> NotifyOrders { get; set; }
    }
}
