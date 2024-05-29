using RentBike.Domain.Entities;
using System.Text.Json.Serialization;

namespace RentBikeUsers.Domain.Entities
{
    public sealed class DeliverymanUser : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Cnpj { get; set; } = string.Empty;        
        public DriversLicense DriversLicense { get; set; }
        [JsonIgnore]
        public ICollection<Rent> Rents { get; set; }
        [JsonIgnore]
        public ICollection<NotifyOrder> NotifyOrders { get; set; }
    }
}
