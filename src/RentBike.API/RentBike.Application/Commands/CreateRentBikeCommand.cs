using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentBike.Application.Commands
{
    public class CreateRentBikeCommand : IRequest
    {
        [Required(ErrorMessage ="DeliverymanUserId is required")]
        public Guid DeliverymanUserId { get; set; }
        [Required(ErrorMessage ="RentPlanId is required")]
        public Guid RentPlanId { get; set; }
    }
}
