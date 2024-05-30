using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentBike.Application.Commands
{
    public class AcceptOrderCommand : IRequest
    {
        [Required(ErrorMessage ="Order ID is required")]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "Deliveryman ID is required")]
        public Guid DeliverymanId { get; set; }
    }
}
