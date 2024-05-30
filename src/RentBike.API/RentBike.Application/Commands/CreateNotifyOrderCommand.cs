using MediatR;

namespace RentBike.Application.Commands
{
    public class CreateNotifyOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
    }
}
