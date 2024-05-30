using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.Application.Queries;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyOrdersController : ControllerBase
    {
        readonly ILogger<NotifyOrdersController> _logger;
        readonly IMediator _mediator;

        public NotifyOrdersController(ILogger<NotifyOrdersController> logger
            , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("orders{orderId}")]
        public async Task<IActionResult> GetNotifiedDeliverymans([FromHeader] Guid user, [FromRoute] Guid orderId, CancellationToken cancellationToken)
        {
            var command = new GetDeliverymansByNotifyOrderQuery { UserId = user, OrderId = orderId };
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
