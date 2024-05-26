using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.Application.Commands;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly ILogger<BikeController> _logger;
        private readonly IMediator _mediator;

        public BikeController(ILogger<BikeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }        

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string? user, [FromBody]CreateBikeCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult(BadRequest(ModelState));
            if(string.IsNullOrEmpty(user))
                return Unauthorized();
            command.AdminUserId = user;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
