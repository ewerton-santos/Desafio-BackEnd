using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentBike.Application.Commands
{
    public class DeleteBikeCommand : IRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string AdminUserId { get; set; }

    }
}
