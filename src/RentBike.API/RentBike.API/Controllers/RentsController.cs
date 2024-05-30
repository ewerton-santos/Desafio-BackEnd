using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.API.Models;
using RentBike.Application.Commands;
using RentBike.Application.Queries;
using RentBike.Domain.Repositories;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        readonly ILogger<RentsController> _logger;
        readonly IMediator _mediator;
        readonly IRentRepository _rentRepository;

        public RentsController(ILogger<RentsController> logger, IMediator mediator, IRentRepository rentRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _rentRepository = rentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentBikeCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("cost/{rentId}")]
        public async Task<IActionResult> GetRentalCost(Guid rentId, [FromQuery] DateTime expectedEndDate, CancellationToken cancellationToken)
        {
            var query = new GetRentCostQuery { RentId = rentId, ExpectedEndDate = expectedEndDate };
            var cost = await _mediator.Send(query, cancellationToken);
            return Ok(new RentCostResponseModel { Cost = cost});
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _rentRepository.GetAll());
    }
}
