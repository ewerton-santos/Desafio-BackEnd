using MediatR;
using RentBike.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentBike.Application.Queries
{
    public class GetBikeByPlateQuery : IRequest<Bike>
    {        
        [RegularExpression(@"^[A-Z]{3}\d{4}$", ErrorMessage = "Invalid plate")]
        public string Plate { get; set; }
        [JsonIgnore]
        public string AdminUserId { get; set; }
    }
}
