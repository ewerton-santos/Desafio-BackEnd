using MediatR;
using RentBike.Domain.Entities;
using System.Text.Json.Serialization;

namespace RentBike.Application.Queries
{
    public class GetAllBikesQuery : IRequest<IEnumerable<Bike>>
    {
        [JsonIgnore]
        public string AdminUserId { get; set; }
    }
}
