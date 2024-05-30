using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Tests.Application.Commands
{
    public class CreateOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidInput_CreatesOrderAndSendsNotification()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var mediatorMock = new Mock<IMediator>();

            var command = new CreateOrderCommand { DeliveryFee = 10 };
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, mediatorMock.Object);

            Order createdOrder = null;
            orderRepositoryMock.Setup(repo => repo.Add(It.IsAny<Order>())).Callback<Order>((order) => createdOrder = order);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            orderRepositoryMock.Verify(repo => repo.Add(It.IsAny<Order>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<CreateNotifyOrderCommand>(c => c.OrderId == createdOrder.Id), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
