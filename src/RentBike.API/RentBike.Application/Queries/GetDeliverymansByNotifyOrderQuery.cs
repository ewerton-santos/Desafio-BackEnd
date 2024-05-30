using MediatR;
using RentBikeUsers.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RentBike.Application.Queries
{
    public class GetDeliverymansByNotifyOrderQuery : IRequest<IEnumerable<DeliverymanUser>>
    {
        [Required(ErrorMessage ="User ID is required")]
        public Guid UserId {  get; set; }
        [Required(ErrorMessage ="Order ID is required")]
        public Guid OrderId { get; set; }
    }
}
