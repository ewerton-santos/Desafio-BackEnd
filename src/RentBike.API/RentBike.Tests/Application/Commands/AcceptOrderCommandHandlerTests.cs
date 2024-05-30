using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Enums;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using System.Linq.Expressions;

namespace RentBike.Tests.Application.Commands
{
    public class AcceptOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidInput_AcceptsOrder()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AcceptOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifierRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var command = new AcceptOrderCommand { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() };
            var handler = new AcceptOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifierRepositoryMock.Object, deliverymanUserRepositoryMock.Object);

            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Order { OrderStatus = OrderStatus.Available });
            orderRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(Enumerable.Empty<Order>());
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(new[] { new Rent { IsActive = true } });
            notifierRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<NotifyOrder, bool>>>())).ReturnsAsync(new List<NotifyOrder>());
            deliverymanUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new DeliverymanUser());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            orderRepositoryMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithOrderAlreadyAccepted_ThrowsDeliverymanCantAcceptOrderException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AcceptOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifierRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var command = new AcceptOrderCommand { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() };
            var handler = new AcceptOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifierRepositoryMock.Object, deliverymanUserRepositoryMock.Object);

            deliverymanUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new DeliverymanUser());
            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Order { OrderStatus = OrderStatus.Available });
            orderRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(new List<Order> { new Order { OrderStatus = OrderStatus.Accepted } });
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(new[] { new Rent { IsActive = true } });
            notifierRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<NotifyOrder, bool>>>())).ReturnsAsync(new List<NotifyOrder>());

            // Act & Assert
            await Assert.ThrowsAsync<DeliverymanCantAcceptOrderException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullDeliveryman_ThrowsDeliverymanUserNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AcceptOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifierRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var command = new AcceptOrderCommand { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() };
            var handler = new AcceptOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifierRepositoryMock.Object, deliverymanUserRepositoryMock.Object);

            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Order { OrderStatus = OrderStatus.Available });
            deliverymanUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((DeliverymanUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<DeliverymanUserNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullOrder_ThrowsOrderNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AcceptOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifierRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var command = new AcceptOrderCommand { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() };
            var handler = new AcceptOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifierRepositoryMock.Object, deliverymanUserRepositoryMock.Object);

            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((Order)null);

            // Act & Assert
            await Assert.ThrowsAsync<OrderNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullRent_ThrowsRentNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AcceptOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifierRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var command = new AcceptOrderCommand { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() };
            var handler = new AcceptOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifierRepositoryMock.Object, deliverymanUserRepositoryMock.Object);

            deliverymanUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new DeliverymanUser());
            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Order { OrderStatus = OrderStatus.Available });
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(Enumerable.Empty<Rent>());

            // Act & Assert
            await Assert.ThrowsAsync<RentNotFoundExeception>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullNotifier_ThrowsDeliverymanWasNotNotifiedException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AcceptOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var notifierRepositoryMock = new Mock<INotifyOrderRepository>();
            var deliverymanUserRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var command = new AcceptOrderCommand { OrderId = Guid.NewGuid(), DeliverymanId = Guid.NewGuid() };
            var handler = new AcceptOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, rentRepositoryMock.Object, notifierRepositoryMock.Object, deliverymanUserRepositoryMock.Object);

            deliverymanUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new DeliverymanUser());
            orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Order { OrderStatus = OrderStatus.Available });
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(new[] { new Rent { IsActive = true } });
            notifierRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<NotifyOrder, bool>>>())).ReturnsAsync((IEnumerable<NotifyOrder>)null);

            // Act & Assert
            await Assert.ThrowsAsync<DeliverymanWasNotNotifiedException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
