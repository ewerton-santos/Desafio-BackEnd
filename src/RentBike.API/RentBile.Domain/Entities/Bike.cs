using RentBikeUsers.Domain.Entities;

namespace RentBike.Domain.Entities
{
    public class Bike : BaseEntity
    {
        public string Plate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
