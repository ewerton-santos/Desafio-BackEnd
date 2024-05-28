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
    public class UpdateBikePlateCommandHandlerTests
    {
        [Fact]
        public async Task Handle_AdminUserNotFound_ThrowsAdminUserNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UpdateBikePlateCommandHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((AdminUser)null);

            var handler = new UpdateBikePlateCommandHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var command = new UpdateBikePlateCommand { AdminUserId = Guid.NewGuid().ToString(), Plate = "oldPlate", NewPlate = "newPlate" };

            // Act & Assert
            await Assert.ThrowsAsync<AdminUserNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BikeNotFound_ThrowsBikeNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UpdateBikePlateCommandHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Bike, bool>>>())).ReturnsAsync(Enumerable.Empty<Bike>());

            var handler = new UpdateBikePlateCommandHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var command = new UpdateBikePlateCommand { AdminUserId = Guid.NewGuid().ToString(), Plate = "oldPlate", NewPlate = "newPlate" };

            // Act & Assert
            await Assert.ThrowsAsync<BikeNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UpdatesBikePlate()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UpdateBikePlateCommandHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            var adminUser = new AdminUser();
            var bike = new Bike { Plate = "oldPlate" };
            var bikes = new List<Bike> { bike };

            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(adminUser);
            bikeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Bike, bool>>>())).ReturnsAsync(bikes);

            var handler = new UpdateBikePlateCommandHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var command = new UpdateBikePlateCommand { AdminUserId = Guid.NewGuid().ToString(), Plate = "oldPlate", NewPlate = "newPlate" };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("newPlate", bike.Plate);
            Assert.NotNull(bike.LastUpdated);
            bikeRepositoryMock.Verify(repo => repo.Update(It.IsAny<Bike>()), Times.Once);
        }
    }
}
