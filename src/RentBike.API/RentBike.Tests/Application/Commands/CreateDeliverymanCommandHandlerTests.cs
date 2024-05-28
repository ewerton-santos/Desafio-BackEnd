using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using RentBikeUsers.Domain.Enums;

namespace RentBike.Tests.Application.Commands
{
    public class CreateDeliverymanCommandHandlerTests
    {
        [Fact]
        public async Task Handle_AddsNewDeliveryman()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateDeliverymanCommandHandler>>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();

            var handler = new CreateDeliverymanCommandHandler(loggerMock.Object, deliverymanRepositoryMock.Object);
            var command = new CreateDeliverymanCommand
            {
                Name = "Pedro",
                Cnpj = "12345678901234",
                Birthdate = new DateTime(1990, 1, 1),
                DriversLicenseNumber = "ABCD1234",
                DriversLicenseType = DriversLicenseType.A
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            deliverymanRepositoryMock.Verify(repo => repo.Add(It.IsAny<DeliverymanUser>()), Times.Once);
        }
    }
}
