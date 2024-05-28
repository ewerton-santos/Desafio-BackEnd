using MediatR;
using RentBike.Application.Queries;
using RentBike.Domain.Repositories;

namespace RentBike.Application.Handlers.Queries
{
    public class GetRentCostQueryHandler : IRequestHandler<GetRentCostQuery, double>
    {
        readonly IRentPlanRepository _rentPlanRepository;
        readonly IRentRepository _rentRepository;
        private const double ADDITIONAL_DAILY_RATE = 50;

        public async Task<double> Handle(GetRentCostQuery request, CancellationToken cancellationToken)
        {
            var rent = await _rentRepository.GetById(request.RentId) ?? throw new Exception("Rent not found");
            if (!rent.IsActive) throw new Exception("This rent has already been completed.");
            var rentplan = await _rentPlanRepository.GetById(rent.RentPlanId) ?? throw new Exception("Rent Plan not found");
            var dateDiffDays = request.ExpectedEndDate.Subtract(rent.StartDate).Days;                        
            if(dateDiffDays > rentplan.Days)
                return (rentplan.Days * rentplan.CostPerDay) + (dateDiffDays * ADDITIONAL_DAILY_RATE);
            return (rentplan.Days * rentplan.CostPerDay) +  ((rentplan.Days - dateDiffDays) * (rentplan.FinePercentage/(double)100.0));
        }
    }
}
