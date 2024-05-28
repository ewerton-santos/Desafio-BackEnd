using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Handlers.Queries;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.Tests.Application.Queries
{
    public class GetRentCostQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCorrectCost_NormalDays()
        {
            // Arrange
            var rentId = Guid.NewGuid();
            var expectedCost = 660.0;
            var rent = new Rent { RentPlanId = Guid.NewGuid(), IsActive = true, StartDate = DateTime.UtcNow.AddDays(-30) };
            var rentPlan = new RentPlan { CostPerDay = 22, Days = 30, FinePercentage = 60 };

            var loggerMock = new Mock<ILogger<GetRentCostQueryHandler>>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            rentRepositoryMock.Setup(repo => repo.GetById(rentId)).ReturnsAsync(rent);
            rentPlanRepositoryMock.Setup(repo => repo.GetById(rent.RentPlanId)).ReturnsAsync(rentPlan);

            var handler = new GetRentCostQueryHandler(loggerMock.Object, rentPlanRepositoryMock.Object, rentRepositoryMock.Object);
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = DateTime.UtcNow };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCost, result);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectCost_ExceededDays()
        {
            // Arrange
            var rentId = Guid.NewGuid();
            var expectedCost = 336.0;
            var rent = new Rent { RentPlanId = Guid.NewGuid(), IsActive = true, StartDate = DateTime.UtcNow.AddDays(-10) };
            var rentPlan = new RentPlan { CostPerDay = 28, Days = 15, FinePercentage = 40 };

            var loggerMock = new Mock<ILogger<GetRentCostQueryHandler>>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            rentRepositoryMock.Setup(repo => repo.GetById(rentId)).ReturnsAsync(rent);
            rentPlanRepositoryMock.Setup(repo => repo.GetById(rent.RentPlanId)).ReturnsAsync(rentPlan);

            var handler = new GetRentCostQueryHandler(loggerMock.Object, rentPlanRepositoryMock.Object, rentRepositoryMock.Object);
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = DateTime.UtcNow };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCost, result);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectCost_EarlyDays()
        {
            // Arrange
            var rentId = Guid.NewGuid();
            var expectedCost = 210.0;
            var rent = new Rent { RentPlanId = Guid.NewGuid(), IsActive = true, StartDate = DateTime.UtcNow.AddDays(-5) };
            var rentPlan = new RentPlan { CostPerDay = 30, Days = 7, FinePercentage = 20 };

            var loggerMock = new Mock<ILogger<GetRentCostQueryHandler>>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            rentRepositoryMock.Setup(repo => repo.GetById(rentId)).ReturnsAsync(rent);
            rentPlanRepositoryMock.Setup(repo => repo.GetById(rent.RentPlanId)).ReturnsAsync(rentPlan);

            var handler = new GetRentCostQueryHandler(loggerMock.Object, rentPlanRepositoryMock.Object, rentRepositoryMock.Object);
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = DateTime.UtcNow.AddDays(2) };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCost, result);
        }

        [Fact]
        public async Task Handle_ThrowsRentNotFoundException()
        {
            // Arrange
            var rentId = Guid.NewGuid();

            var loggerMock = new Mock<ILogger<GetRentCostQueryHandler>>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            rentRepositoryMock.Setup(repo => repo.GetById(rentId)).ReturnsAsync((Rent)null);

            var handler = new GetRentCostQueryHandler(loggerMock.Object, rentPlanRepositoryMock.Object, rentRepositoryMock.Object);
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = DateTime.UtcNow };

            // Act & Assert
            await Assert.ThrowsAsync<RentNotFoundExeception>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsRentAlreadyCompletedException()
        {
            // Arrange
            var rentId = Guid.NewGuid();
            var rent = new Rent { IsActive = false };

            var loggerMock = new Mock<ILogger<GetRentCostQueryHandler>>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            rentRepositoryMock.Setup(repo => repo.GetById(rentId)).ReturnsAsync(rent);

            var handler = new GetRentCostQueryHandler(loggerMock.Object, rentPlanRepositoryMock.Object, rentRepositoryMock.Object);
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = DateTime.UtcNow };

            // Act & Assert
            await Assert.ThrowsAsync<RentAlreadyCompletedException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsRentPlanNotFoundException()
        {
            // Arrange
            var rentId = Guid.NewGuid();

            var loggerMock = new Mock<ILogger<GetRentCostQueryHandler>>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            rentRepositoryMock.Setup(repo => repo.GetById(rentId)).ReturnsAsync(new Rent());
            rentPlanRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((RentPlan)null);

            var handler = new GetRentCostQueryHandler(loggerMock.Object, rentPlanRepositoryMock.Object, rentRepositoryMock.Object);
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = DateTime.UtcNow };

            // Act & Assert
            await Assert.ThrowsAsync<RentPlanNotFoundExeception>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
