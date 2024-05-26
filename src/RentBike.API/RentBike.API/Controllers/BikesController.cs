using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.Application.Commands;
using RentBike.Application.Queries;
using RentBike.Domain.Repositories;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly ILogger<BikesController> _logger;
        private readonly IBikeRepository _bikeRepository;
        private readonly IMediator _mediator;

        public BikesController(ILogger<BikesController> logger, IMediator mediator, IBikeRepository bikeRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _bikeRepository = bikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string user, CancellationToken cancellationToken)
        {
            var command = new GetAllBikesQuery { AdminUserId = user };
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpGet("{plate}")]
        public async Task<IActionResult> GetbyPlate([FromHeader] string? user, [FromRoute] string plate, CancellationToken cancellationToken)
        {
            var command = new GetBikeByPlateQuery { Plate = plate, AdminUserId = user };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (string.IsNullOrWhiteSpace(user))
                return Unauthorized();
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string? user, [FromBody] CreateBikeCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (string.IsNullOrWhiteSpace(user)) return Unauthorized();
            command.AdminUserId = user;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePlate([FromHeader] string? user, [FromBody] UpdateBikePlateCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (string.IsNullOrWhiteSpace(user)) return Unauthorized();
            command.AdminUserId = user;
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromHeader] string user, [FromRoute] string id, CancellationToken cancellationToken) 
        {
            var command = new DeleteBikeCommand { Id = Guid.Parse(id), AdminUserId = user };
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            if (string.IsNullOrWhiteSpace(user)) return Unauthorized();
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
