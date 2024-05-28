using MediatR;
using Microsoft.Extensions.Logging;
using RentBike.Application.Queries;
using RentBike.Domain.Entities;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Queries
{
    public class GetRentCostQueryHandler : IRequestHandler<GetRentCostQuery, double>
    {
        readonly ILogger<GetRentCostQueryHandler> _logger;
        readonly IRentPlanRepository _rentPlanRepository;
        readonly IRentRepository _rentRepository;
        private const double ADDITIONAL_DAILY_RATE = 50;

        public GetRentCostQueryHandler(ILogger<GetRentCostQueryHandler> logger
            , IRentPlanRepository rentPlanRepository, IRentRepository rentRepository)
        {
            _logger = logger;
            _rentRepository = rentRepository;
            _rentPlanRepository = rentPlanRepository;
        }

        public async Task<double> Handle(GetRentCostQuery request, CancellationToken cancellationToken)
        {
            var rent = await _rentRepository.GetById(request.RentId) ?? throw new Exception("Rent not found");
            if (!rent.IsActive) throw new Exception("This rent has already been completed.");
            var rentplan = await _rentPlanRepository.GetById(rent.RentPlanId) ?? throw new Exception("Rent Plan not found");
            var dailyUsed = request.ExpectedEndDate.Subtract(rent.StartDate).Days;
            if (dailyUsed > rentplan.Days)
                return CalculateCostExceededPlanDays(rentplan, dailyUsed);
            else if (dailyUsed < rentplan.Days)
                return CalculateCostEarlyPlanDays(rentplan, dailyUsed);
            else
                return CalculateNormalCostPlanDays(rentplan);
        }

        private double CalculateNormalCostPlanDays(RentPlan rentplan) => rentplan.CostPerDay * rentplan.Days;

        private double CalculateCostExceededPlanDays(RentPlan rentplan, int dailyUsed)
        {
            var normalCost = CalculateNormalCostPlanDays(rentplan);
            var addictionalCost = (dailyUsed - rentplan.Days) * ADDITIONAL_DAILY_RATE;
            return normalCost + addictionalCost;
        }

        private double CalculateCostEarlyPlanDays(RentPlan rentplan, int dailyUsed)
        {
            var dailyUsedCost = dailyUsed * rentplan.CostPerDay;
            var addictionalCost = (((rentplan.Days - dailyUsed) * rentplan.CostPerDay) * rentplan.FinePercentage) / 100.0;
            return dailyUsedCost + addictionalCost;
        }
    }
}
