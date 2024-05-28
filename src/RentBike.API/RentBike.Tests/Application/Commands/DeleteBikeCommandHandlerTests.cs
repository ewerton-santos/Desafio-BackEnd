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
    public class DeleteBikeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_AdminUserNotFound_ThrowsAdminUserNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((AdminUser)null);

            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object);
            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };

            // Act & Assert
            await Assert.ThrowsAsync<AdminUserNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BikeNotFound_ThrowsBikeNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((Bike)null);

            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object);
            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };

            // Act & Assert
            await Assert.ThrowsAsync<BikeNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RemovesBike()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Bike());

            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object);
            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            bikeRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Bike>()), Times.Once);
        }
    }
}
