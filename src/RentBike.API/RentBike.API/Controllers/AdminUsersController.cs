using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentBike.API.Models;
using RentBike.Application.Queries;
using RentBike.Domain.Repositories;
using RentBikeUsers.Domain.Entities;

namespace RentBike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly ILogger<AdminUsersController> _logger;
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="adminUserRepository"></param>
        public AdminUsersController(ILogger<AdminUsersController> logger, IMediator mediator, IAdminUserRepository adminUserRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _adminUserRepository = adminUserRepository;
        }

        // GET: api/<AdminUsersController>                
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _adminUserRepository.GetAll());
        }

        // GET api/<AdminUsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            //return Ok(await _adminUserRepository.GetById(id));
            return Ok(await _mediator.Send(new GetAdminUserQuery { Id = id }, cancellationToken));
        }

        // POST api/<AdminUsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminUserInsertModel model)
        {
            await _adminUserRepository.Add(new AdminUser { Name = model.Name });
            return Created();
        }

        // PUT api/<AdminUsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AdminUserInsertModel model)
        {
            var entity = await _adminUserRepository.GetById(id);
            if(entity == null) 
                return NotFound();
            entity.Name = model.Name;
            await _adminUserRepository.Update(entity);
            return NoContent();
        }

        // DELETE api/<AdminUsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _adminUserRepository.GetById(id);
            if( entity == null)
                return NotFound();
            await _adminUserRepository.Remove(entity);
            return NoContent();
        }
    }
}
