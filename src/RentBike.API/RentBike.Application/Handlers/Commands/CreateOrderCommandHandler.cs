using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Enums;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        readonly ILogger<CreateOrderCommandHandler> _logger;        
        readonly IOrderRepository _orderRepository;
        readonly IMediator _mediator;

        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger
            , IOrderRepository orderRepository, IMediator mediator)
        {
            _logger = logger;            
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                DeliveryFee = request.DeliveryFee,
                OrderStatus = OrderStatus.Available
            };
            await _orderRepository.Add(order);
            await _mediator.Send(new CreateNotifyOrderCommand { OrderId = order.Id });
        }
    }
}
