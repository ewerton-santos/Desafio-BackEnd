using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Handlers.Queries;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Tests.Application.Queries
{
    public class GetAllBikesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsAllBikes()
        {
            // Arrange
            var adminUserId = Guid.NewGuid();
            var bikeA = new Bike { Model = "Model 1", Plate = "Plate 1" };
            var bikeB = new Bike { Model = "Model 2", Plate = "Plate 2" };
            var expectedBikes = new List<Bike> { bikeA, bikeB };
            
            var loggerMock = new Mock<ILogger<GetAllBikesQueryHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(adminUserId)).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedBikes);

            var handler = new GetAllBikesQueryHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object);
            var query = new GetAllBikesQuery { AdminUserId = adminUserId.ToString() };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBikes.Count, result.Count());
            foreach (var expectedBike in expectedBikes)
            {
                Assert.Contains(expectedBike, result);
            }
        }

        [Fact]
        public async Task Handle_ThrowsAdminUserNotFoundException()
        {
            // Arrange
            var adminUserId = Guid.NewGuid();

            var loggerMock = new Mock<ILogger<GetAllBikesQueryHandler>>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(adminUserId)).ReturnsAsync((AdminUser)null);

            var handler = new GetAllBikesQueryHandler(loggerMock.Object, adminUserRepositoryMock.Object, bikeRepositoryMock.Object);
            var query = new GetAllBikesQuery { AdminUserId = adminUserId.ToString() };

            // Act & Assert
            await Assert.ThrowsAsync<AdminUserNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
