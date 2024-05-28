using Moq;
using RentBike.Application.Handlers.Queries;
using RentBike.Application.Queries;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Tests.Application.Queries
{
    public class GetAdminUserQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsAdminUser()
        {
            // Arrange
            var expectedAdminUser = new AdminUser { Name = "Test Admin User" };
            var adminUserId = expectedAdminUser.Id;

            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            adminUserRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(expectedAdminUser);

            var handler = new GetAdminUserQueryHandler(adminUserRepositoryMock.Object);
            var query = new GetAdminUserQuery { Id = adminUserId };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAdminUser.Id, result.Id);
            Assert.Equal(expectedAdminUser.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ReturnsNullWhenAdminUserNotFound()
        {
            // Arrange
            var adminUserId = Guid.NewGuid();

            var adminUserRepositoryMock = new Mock<IAdminUserRepository>();
            adminUserRepositoryMock.Setup(repo => repo.GetById(adminUserId)).ReturnsAsync((AdminUser)null);

            var handler = new GetAdminUserQueryHandler(adminUserRepositoryMock.Object);
            var query = new GetAdminUserQuery { Id = adminUserId };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
