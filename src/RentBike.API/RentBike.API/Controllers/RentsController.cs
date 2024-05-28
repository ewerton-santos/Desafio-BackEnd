using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.API.Models;
using RentBike.Application.Commands;
using RentBike.Application.Queries;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        readonly ILogger<RentsController> _logger;
        readonly IMediator _mediator;

        public RentsController(ILogger<RentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentBikeCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("rentId")]
        public async Task<IActionResult> GetRentalCost(Guid rentId, [FromQuery] DateTime expectedEndDate, CancellationToken cancellationToken)
        {
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = expectedEndDate };
            var cost = await _mediator.Send(query, cancellationToken);
            return Ok(new RentCostResponseModel { Cost = cost});
        }
    }
}
