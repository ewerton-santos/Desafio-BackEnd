using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Tests.Application.Commands
{
    public class CreateBikeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_AdminUserNotFound_ThrowsAdminUserNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateBikeCommandHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((AdminUser)null);

            var handler = new CreateBikeCommandHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var command = new CreateBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Model = "Test Model", Year = 2022, Plate = "ABC1234" };

            // Act & Assert
            await Assert.ThrowsAsync<AdminUserNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_AdminUserFound_AddsNewBike()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateBikeCommandHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.Add(It.IsAny<Bike>()));

            var handler = new CreateBikeCommandHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var command = new CreateBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Model = "Test Model", Year = 2022, Plate = "ABC1234" };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            bikeRepositoryMock.Verify(repo => repo.Add(It.IsAny<Bike>()), Times.Once);
        }
    }
}
