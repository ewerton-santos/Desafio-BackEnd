using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Enums;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Commands
{
    public class CreateNotifyOrderCommandHandler : IRequestHandler<CreateNotifyOrderCommand>
    {
        readonly ILogger<CreateNotifyOrderCommandHandler> _logger;
        readonly IDeliverymanUserRepository _deliverymanUserRepository;
        readonly IOrderRepository _orderRepository;
        readonly IRentRepository _rentRepository;
        readonly INotifyOrderRepository _notifyOrderRepository;

        public CreateNotifyOrderCommandHandler(ILogger<CreateNotifyOrderCommandHandler> logger
            ,IDeliverymanUserRepository deliverymanUserRepository, IOrderRepository orderRepository
            , IRentRepository rentRepository, INotifyOrderRepository notifyOrderRepository)
        {
            _logger = logger;
            _deliverymanUserRepository = deliverymanUserRepository;
            _orderRepository = orderRepository;
            _rentRepository = rentRepository;
            _notifyOrderRepository = notifyOrderRepository;
        }

        public async Task Handle(CreateNotifyOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId) ?? throw new ArgumentNullException(nameof(request.OrderId));
            var activeRents = await _rentRepository.Find(p => p.IsActive);
            var orders = await _orderRepository.Find(x => x.OrderStatus == OrderStatus.Accepted);
            var deliverymans = await _deliverymanUserRepository.Find(p => activeRents.Select(q => q.DeliverymanUserId).Contains(p.Id));
            deliverymans = deliverymans.Where(p => !orders.Select(q => q.DeliverymanId).Contains(p.Id)).ToList();
            Parallel.ForEach(deliverymans, q =>
            {
                _notifyOrderRepository.Add(new NotifyOrder
                {
                    OrderId = order.Id,
                    DeliverymanId = q.Id,
                });
            });
        }
    }
}
