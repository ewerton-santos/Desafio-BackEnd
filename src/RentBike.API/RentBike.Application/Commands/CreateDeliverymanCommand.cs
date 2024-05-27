using MediatR;
using RentBikeUsers.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentBike.Application.Commands
{
    public class CreateDeliverymanCommand : IRequest
    {
        [Required(ErrorMessage= "The name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="CNPJ is required")]
        public string Cnpj { get; set; }
        [Required(ErrorMessage ="Birthdate is required")]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Driver's license number is required")]
        public string DriversLicenseNumber { get; set; }
        [Required(ErrorMessage = "Driver's license type is required")]        
        public DriversLicenseType DriversLicenseType { get; set; }        
    }
}
