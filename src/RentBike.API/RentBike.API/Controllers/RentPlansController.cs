using Microsoft.AspNetCore.Mvc;
using RentBike.Domain.Repositories;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentPlansController : ControllerBase
    {
        readonly ILogger<RentPlansController> _logger;
        readonly IRentPlanRepository _rentPlanRepository;

        public RentPlansController(ILogger<RentPlansController> logger
            , IRentPlanRepository rentPlanRepository)
        {
            _logger = logger;
            _rentPlanRepository = rentPlanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _rentPlanRepository.GetAll());
    }
}
