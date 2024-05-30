using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentBike.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        [Required]
        public double DeliveryFee { get; set; }
    }
}
