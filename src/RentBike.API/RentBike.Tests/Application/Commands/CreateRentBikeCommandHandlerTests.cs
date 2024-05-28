using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using RentBike.Application.Commands;
using RentBike.Application.Handlers.Commands;
using RentBike.Domain.Entities;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;
using RentBikeUsers.Domain.Enums;
using System.Linq.Expressions;

namespace RentBike.Tests.Application.Commands
{
    public class CreateRentBikeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeliverymanNotFound_ThrowsDeliverymanUserNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRentBikeCommandHandler>>();
            var mediatorMock = new Mock<IMediator>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            deliverymanRepositoryMock.Setup(repo => repo
            .GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<DeliverymanUser, object>>>()))
            .ReturnsAsync((DeliverymanUser)null);

            var handler = new CreateRentBikeCommandHandler(loggerMock.Object, mediatorMock.Object,
                                                          bikeRepositoryMock.Object, deliverymanRepositoryMock.Object,
                                                          rentRepositoryMock.Object, rentPlanRepositoryMock.Object);

            var command = new CreateRentBikeCommand();

            // Act & Assert
            await Assert.ThrowsAsync<DeliverymanUserNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeliverymanWithoutDriversLicense_ThrowsDriversLicenseNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRentBikeCommandHandler>>();
            var mediatorMock = new Mock<IMediator>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            deliverymanRepositoryMock.Setup(repo => repo
            .GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<DeliverymanUser, object>>[]>()))
                .ReturnsAsync(new DeliverymanUser() { Name = "Nascimento" });

            var handler = new CreateRentBikeCommandHandler(loggerMock.Object, mediatorMock.Object,
                                                          bikeRepositoryMock.Object, deliverymanRepositoryMock.Object,
                                                          rentRepositoryMock.Object, rentPlanRepositoryMock.Object);

            var command = new CreateRentBikeCommand();

            // Act & Assert
            await Assert.ThrowsAsync<DriversLicenseNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeliverymanWithDriversLicenseTypeB_ThrowsDriverNotQualifiedForCategoryException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRentBikeCommandHandler>>();
            var mediatorMock = new Mock<IMediator>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            deliverymanRepositoryMock.Setup(repo => repo
            .GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<DeliverymanUser, object>>[]>())).ReturnsAsync(new DeliverymanUser
            {
                Cnpj = "12345667788",
                Name = "Marcos",
                DriversLicense = new DriversLicense { DriversLicenseType = DriversLicenseType.B }
            });

            var handler = new CreateRentBikeCommandHandler(loggerMock.Object, mediatorMock.Object,
                                                          bikeRepositoryMock.Object, deliverymanRepositoryMock.Object,
                                                          rentRepositoryMock.Object, rentPlanRepositoryMock.Object);

            var command = new CreateRentBikeCommand();

            // Act & Assert
            await Assert.ThrowsAsync<DriverNotQualifiedForCategoryException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RentPlanNotFound_ThrowsRentPlanNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRentBikeCommandHandler>>();
            var mediatorMock = new Mock<IMediator>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            deliverymanRepositoryMock.Setup(repo => repo
            .GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<DeliverymanUser, object>>[]>()))
                .ReturnsAsync(new DeliverymanUser
            {
                DriversLicense = new DriversLicense { DriversLicenseType = DriversLicenseType.A }
            });

            rentPlanRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((RentPlan)null);

            var handler = new CreateRentBikeCommandHandler(loggerMock.Object, mediatorMock.Object,
                                                          bikeRepositoryMock.Object, deliverymanRepositoryMock.Object,
                                                          rentRepositoryMock.Object, rentPlanRepositoryMock.Object);

            var command = new CreateRentBikeCommand();

            // Act & Assert
            await Assert.ThrowsAsync<RentPlanNotFoundExeception>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NoAvailableBikes_ThrowsBikeNotAvailableException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRentBikeCommandHandler>>();
            var mediatorMock = new Mock<IMediator>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            deliverymanRepositoryMock.Setup(repo => repo
            .GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<DeliverymanUser, object>>[]>()))
            .ReturnsAsync(new DeliverymanUser
            {
                DriversLicense = new DriversLicense { DriversLicenseType = DriversLicenseType.A }
            });

            rentPlanRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new RentPlan());

            bikeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Bike, bool>>>())).ReturnsAsync(Enumerable.Empty<Bike>());

            var handler = new CreateRentBikeCommandHandler(loggerMock.Object, mediatorMock.Object,
                                                          bikeRepositoryMock.Object, deliverymanRepositoryMock.Object,
                                                          rentRepositoryMock.Object, rentPlanRepositoryMock.Object);

            var command = new CreateRentBikeCommand();

            // Act & Assert
            await Assert.ThrowsAsync<BikeNotAvailableException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_AddsNewRent()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRentBikeCommandHandler>>();
            var mediatorMock = new Mock<IMediator>();
            var bikeRepositoryMock = new Mock<IBikeRepository>();
            var deliverymanRepositoryMock = new Mock<IDeliverymanUserRepository>();
            var rentRepositoryMock = new Mock<IRentRepository>();
            var rentPlanRepositoryMock = new Mock<IRentPlanRepository>();

            deliverymanRepositoryMock.Setup(repo => repo
            .GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<DeliverymanUser, object>>[]>()))
                .ReturnsAsync(new DeliverymanUser
            {                
                DriversLicense = new DriversLicense { DriversLicenseType = DriversLicenseType.A }
            });

            rentPlanRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new RentPlan());

            bikeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Bike, bool>>>())).ReturnsAsync(new List<Bike> { new Bike() });

            var handler = new CreateRentBikeCommandHandler(loggerMock.Object, mediatorMock.Object,
                                                          bikeRepositoryMock.Object, deliverymanRepositoryMock.Object,
                                                          rentRepositoryMock.Object, rentPlanRepositoryMock.Object);

            var command = new CreateRentBikeCommand();

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            rentRepositoryMock.Verify(repo => repo.Add(It.IsAny<Rent>()), Times.Once);
        }
    }
}