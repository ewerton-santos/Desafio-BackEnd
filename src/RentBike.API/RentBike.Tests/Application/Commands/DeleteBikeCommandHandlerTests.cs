using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using System.Linq.Expressions;

namespace RentBike.Tests.Application.Commands
{
    public class DeleteBikeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidInput_RemovesBike()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();

            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };
            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object, rentRepositoryMock.Object);

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Bike());
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(Enumerable.Empty<Rent>());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            bikeRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Bike>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithRent_ThrowsRemoveBikeException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();

            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };
            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object, rentRepositoryMock.Object);

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Bike());
            rentRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Rent, bool>>>())).ReturnsAsync(new[] { new Rent() });

            // Act & Assert
            await Assert.ThrowsAsync<RemoveBikeException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullAdminUser_ThrowsAdminUserNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();

            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };
            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object, rentRepositoryMock.Object);

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((AdminUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<AdminUserNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullBike_ThrowsBikeNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteBikeCommandHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();

            var command = new DeleteBikeCommand { AdminUserId = Guid.NewGuid().ToString(), Id = Guid.NewGuid() };
            var handler = new DeleteBikeCommandHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object, rentRepositoryMock.Object);

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((Bike)null);

            // Act & Assert
            await Assert.ThrowsAsync<BikeNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
