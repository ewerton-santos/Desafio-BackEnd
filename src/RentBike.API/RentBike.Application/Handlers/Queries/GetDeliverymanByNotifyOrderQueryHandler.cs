using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Queries;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Application.Handlers.Queries
{
    public class GetDeliverymanByNotifyOrderQueryHandler : IRequestHandler<GetDeliverymansByNotifyOrderQuery, IEnumerable<DeliverymanUser>>
    {
        readonly ILogger<GetDeliverymanByNotifyOrderQueryHandler> _logger;
        readonly IAdminUserRepository _adminUserRepository;
        readonly INotifyOrderRepository _notifyOrderRepository;
        readonly IDeliverymanUserRepository _deliverymanUserRepository;

        public GetDeliverymanByNotifyOrderQueryHandler(ILogger<GetDeliverymanByNotifyOrderQueryHandler> logger,
            IAdminUserRepository adminUserRepository, INotifyOrderRepository notifyOrderRepository,
            IDeliverymanUserRepository deliverymanUserRepository)
        {
            _logger = logger;
            _adminUserRepository = adminUserRepository;
            _notifyOrderRepository = notifyOrderRepository;
            _deliverymanUserRepository = deliverymanUserRepository;
        }

        public async Task<IEnumerable<DeliverymanUser>> Handle(GetDeliverymansByNotifyOrderQuery request, CancellationToken cancellationToken)
        {
            _ = await _adminUserRepository.GetById(request.UserId) ?? throw new AdminUserNotFoundException();
            var notityOrders = await _notifyOrderRepository.Find(p => p.OrderId == request.OrderId);
            var deliverymans = await _deliverymanUserRepository.Find(p => notityOrders.Select(x => x.DeliverymanId).Contains(p.Id));
            return deliverymans;
        }
    }
}
