using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentBike.Application.Commands
{
    public class UpdateBikePlateCommand : IRequest
    {
        [Required(ErrorMessage = "Plate is required field")]
        [RegularExpression(@"^[A-Z]{3}\d{4}$", ErrorMessage = "Invalid plate")]
        public string Plate { get; set; }
        [Required(ErrorMessage = "Plate is required field")]
        [RegularExpression(@"^[A-Z]{3}\d{4}$", ErrorMessage = "Invalid new plate")]
        public string NewPlate { get; set; }
        [JsonIgnore]
        public string AdminUserId { get; set; }
    }
}
