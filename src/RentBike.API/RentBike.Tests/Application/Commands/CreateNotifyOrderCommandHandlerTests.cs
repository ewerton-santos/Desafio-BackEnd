using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using System.Linq.Expressions;

namespace RentBike.Tests.Application.Commands
{
    public class CreateNotifyOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidInput_CreatesNotificationsForAvailableDeliverymen()
        {
            // Arrange
            var order = new Order();
            var loggerMock = new Mock<ILogger<CreateNotifyOrderCommandHandler>>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifyOrderRepositoryMock = new Mock<INotifyOrderRepository>();

            var command = new CreateNotifyOrderCommand { OrderId = order.Id };
            var handler = new CreateNotifyOrderCommandHandler(loggerMock.Object, deliverymanUserRepositoryMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifyOrderRepositoryMock.Object);

            //var order = new Order { Id = command.OrderId };
            
            var activeRents = new List<Rent> { new Rent { IsActive = true, DeliverymanUserId = Guid.NewGuid() } };
            var orders = Enumerable.Empty<Order>();
            var deliveryman = new List<DeliverymanUser> { new DeliverymanUser {  } };

            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(order);
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(activeRents);
            orderRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(orders);
            deliverymanUserRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<DeliverymanUser, bool>>>())).ReturnsAsync(deliveryman);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            notifyOrderRepositoryMock.Verify(repo => repo.Add(It.IsAny<NotifyOrder>()), Times.Exactly(deliveryman.Count));
        }

        [Fact]
        public async Task Handle_WithNoAvailableDeliverymen_DoesNotCreateNotifications()
        {
            // Arrange
            var order = new Order();
            var loggerMock = new Mock<ILogger<CreateNotifyOrderCommandHandler>>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifyOrderRepositoryMock = new Mock<INotifyOrderRepository>();

            var command = new CreateNotifyOrderCommand { OrderId = order.Id };
            var handler = new CreateNotifyOrderCommandHandler(loggerMock.Object, deliverymanUserRepositoryMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifyOrderRepositoryMock.Object);

            
            var activeRents = new List<Rent> { new Rent { IsActive = true, DeliverymanUserId = Guid.NewGuid() } };
            var orders = new List<Order> { new Order { DeliverymanId = Guid.NewGuid() } };
            var deliverymen = Enumerable.Empty<DeliverymanUser>();

            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(order);
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(activeRents);
            orderRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(orders);
            deliverymanUserRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<DeliverymanUser, bool>>>())).ReturnsAsync(deliverymen);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            notifyOrderRepositoryMock.Verify(repo => repo.Add(It.IsAny<NotifyOrder>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WithNullOrder_ThrowsArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateNotifyOrderCommandHandler>>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifyOrderRepositoryMock = new Mock<INotifyOrderRepository>();

            var command = new CreateNotifyOrderCommand { OrderId = Guid.NewGuid() };
            var handler = new CreateNotifyOrderCommandHandler(loggerMock.Object, deliverymanUserRepositoryMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifyOrderRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
