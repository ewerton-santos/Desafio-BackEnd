using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.API.Models;
using RentBike.Application.Services.Interfaces;
using RentBike.Domain.Exceptions;
using RentBike.Domain.Repositories;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly ILogger<OrdersController> _logger;
        readonly IMediator _mediator;
        readonly IRabbitPublisherService _rabbitPublisherService;
        readonly IAdminUserRepository _adminUserRepository;
        readonly IOrderRepository _orderRepository;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator
            , IRabbitPublisherService rabbitPublisherService, IAdminUserRepository adminUserRepository
            , IOrderRepository orderRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _rabbitPublisherService = rabbitPublisherService;
            _adminUserRepository = adminUserRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string? user, [FromBody] CreateOrderInsertModel model)
        {
            if (string.IsNullOrWhiteSpace(user)) return Unauthorized();
            var userAdmin = await _adminUserRepository.GetById(Guid.Parse(user)) ?? throw new AdminUserNotFoundException();
            _rabbitPublisherService.PublishMessage(model);
            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string? user) 
        {
            if (string.IsNullOrWhiteSpace(user)) return Unauthorized();
            _ = await _adminUserRepository.GetById(Guid.Parse(user)) ?? throw new AdminUserNotFoundException();
            return Ok(await _orderRepository.GetAll());
        }
    }
}
