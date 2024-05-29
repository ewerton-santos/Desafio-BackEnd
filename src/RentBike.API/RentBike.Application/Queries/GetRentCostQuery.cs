using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentBike.Application.Queries
{
    public class GetRentCostQuery : IRequest<double>
    {
        public Guid RentId { get; set; }
        [Required(ErrorMessage = "Expected End Date is required")]
        public DateTime ExpectedEndDate { get; set; }
    }
}
