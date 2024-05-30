using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Handlers.Queries;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using System.Linq.Expressions;

namespace RentBike.Tests.Application.Queries
{
    public class GetDeliverymanByOrderQueryHandlerTests
    {
        [Fact]       
        public async Task Handle_WithValidInput_ReturnsDeliverymen()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GetDeliverymanByNotifyOrderQueryHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var notifyOrderRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var handler = new GetDeliverymanByNotifyOrderQueryHandler(
                loggerMock.Object,
                adminUserRepositoryMock.Object,
                notifyOrderRepositoryMock.Object,
                deliverymanUserRepositoryMock.Object
            );

            var query = new GetDeliverymansByNotifyOrderQuery
            {
                UserId = Guid.NewGuid(),
                OrderId = Guid.NewGuid()
            };

            var deliverymans = new List<DeliverymanUser>
            {
                new DeliverymanUser {  },
                new DeliverymanUser {  }
            };

            var notifyOrders = new List<NotifyOrder>
            {
                new NotifyOrder { OrderId = query.OrderId, DeliverymanId = deliverymans[0].Id },
                new NotifyOrder { OrderId = query.OrderId, DeliverymanId = deliverymans[1].Id },
                new NotifyOrder { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() } // Not related to the query's OrderId
            };            

            adminUserRepositoryMock.Setup(repo => repo.GetById(query.UserId)).ReturnsAsync(new AdminUser());            
            notifyOrderRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<NotifyOrder, bool>>>()))
                .ReturnsAsync(notifyOrders);
            deliverymanUserRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<DeliverymanUser, bool>>>()))
                .ReturnsAsync(deliverymans);
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, d => d.Id == notifyOrders[0].DeliverymanId);
            Assert.Contains(result, d => d.Id == notifyOrders[1].DeliverymanId);
        }
    }
}
