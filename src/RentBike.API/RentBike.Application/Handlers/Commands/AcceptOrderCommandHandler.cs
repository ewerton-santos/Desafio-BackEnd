using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Enums;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class AcceptOrderCommandHandler : IRequestHandler<AcceptOrderCommand>
    {
        readonly ILogger<AcceptOrderCommandHandler> _logger;
        readonly IOrderRepository _orderRepository;
        readonly IRentRepository _rentRepository;
        readonly INotifyOrderRepository _notifierRepository;
        readonly IDeliverymanUserRepository _deliverymanUserRepository;

        public AcceptOrderCommandHandler(ILogger<AcceptOrderCommandHandler> logger
            , IOrderRepository orderRepository, IRentRepository rentRepository
            , INotifyOrderRepository notifyOrderRepository, IDeliverymanUserRepository deliverymanUserRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _rentRepository = rentRepository;
            _notifierRepository = notifyOrderRepository;
            _deliverymanUserRepository = deliverymanUserRepository;
        }

        public async Task Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId) ?? throw new OrderNotFoundException();
            if(order.OrderStatus != OrderStatus.Available) 
                throw new OrderNotAvailableException();
            var orders = await _orderRepository.Find(p => p.OrderStatus == OrderStatus.Accepted && p.Id == request.DeliverymanId);
            if(orders.Any()) 
                throw new DeliverymanCantAcceptOrderException();
            var deliveryman = await _deliverymanUserRepository.GetById(request.DeliverymanId) ?? throw new DeliverymanUserNotFoundException();
            var rent = (await _rentRepository
                .Find(p => p.DeliverymanUserId == deliveryman.Id && p.IsActive))
                .FirstOrDefault() ?? throw new RentNotFoundExeception();
            _ = await _notifierRepository.Find(p => p.OrderId == p.OrderId && p.DeliverymanId == deliveryman.Id) ?? throw new DeliverymanWasNotNotifiedException();
            
            order.LastUpdated = DateTime.UtcNow;
            order.DeliverymanId = deliveryman.Id;
            order.OrderStatus = OrderStatus.Accepted;
            await _orderRepository.Update(order);
        }
    }
}
