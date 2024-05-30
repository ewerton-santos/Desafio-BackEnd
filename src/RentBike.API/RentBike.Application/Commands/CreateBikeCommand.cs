using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentBike.Application.Commands
{
    public class CreateBikeCommand : IRequest
    {
        [Required(ErrorMessage = "Model can't be empty")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Year is required field")]
        [Range(2000, 9999, ErrorMessage = "Invalid Year")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Plate is required field")]
        [RegularExpression(@"^[A-Z]{3}\d{4}$", ErrorMessage = "Invalid plate")]
        public string Plate { get; set; }
        [JsonIgnore]        
        public string AdminUserId { get; set; }
    }
}
