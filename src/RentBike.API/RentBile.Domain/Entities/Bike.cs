using RentBikeUsers.Domain.Entities;
using System.Text.Json.Serialization;

namespace RentBike.Domain.Entities
{
    public class Bike : BaseEntity
    {
        public string Plate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        [JsonIgnore]
        public ICollection<Rent> Rents { get; set; }
    }
}
