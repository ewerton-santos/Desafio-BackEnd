using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.Application.Commands;
using RentBike.Domain.Repositories;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverymansController : ControllerBase
    {
        readonly ILogger<DeliverymansController> _logger;
        readonly IDeliverymanUserRepository _deliverymanUserRepository;
        readonly IMediator _mediator;

        public DeliverymansController(ILogger<DeliverymansController> logger
            , IDeliverymanUserRepository deliverymanUserRepository, IMediator mediator)
        {
            _logger = logger;
            _deliverymanUserRepository = deliverymanUserRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _deliverymanUserRepository.GetAll(c => c.DriversLicense));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDeliverymanCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

    }
}
