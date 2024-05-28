using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Handlers.Queries;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using System.Linq.Expressions;

namespace RentBike.Tests.Application.Queries
{
    public class GetBikeByPlateQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsBike()
        {
            // Arrange
            var adminUserId = Guid.NewGuid();
            var expectedBike = new Bike { Plate = "ABC123", Model = "Test Model" };

            var loggerMock = new Mock<ILogger<GetBikeByPlateQueryHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(adminUserId)).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Bike, bool>>>())).ReturnsAsync(new[] { expectedBike });

            var handler = new GetBikeByPlateQueryHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var query = new GetBikeByPlateQuery { AdminUserId = adminUserId.ToString(), Plate = "ABC123" };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBike.Id, result.Id);
            Assert.Equal(expectedBike.Plate, result.Plate);
            Assert.Equal(expectedBike.Model, result.Model);
        }

        [Fact]
        public async Task Handle_ReturnsNullWhenAdminUserNotFound()
        {
            // Arrange
            var adminUserId = Guid.NewGuid();

            var loggerMock = new Mock<ILogger<GetBikeByPlateQueryHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(adminUserId)).ReturnsAsync((AdminUser)null);

            var handler = new GetBikeByPlateQueryHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var query = new GetBikeByPlateQuery { AdminUserId = adminUserId.ToString(), Plate = "ABC123" };

            // Act & Assert
            await Assert.ThrowsAsync<AdminUserNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ReturnsNullWhenNoBikeFound()
        {
            // Arrange
            var adminUserId = Guid.NewGuid();

            var loggerMock = new Mock<ILogger<GetBikeByPlateQueryHandler>>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();

            adminUserRepositoryMock.Setup(repo => repo.GetById(adminUserId)).ReturnsAsync(new AdminUser());
            bikeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Bike, bool>>>())).ReturnsAsync(Enumerable.Empty<Bike>());

            var handler = new GetBikeByPlateQueryHandler(loggerMock.Object, bikeRepositoryMock.Object, adminUserRepositoryMock.Object);
            var query = new GetBikeByPlateQuery { AdminUserId = adminUserId.ToString(), Plate = "ABC123" };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
