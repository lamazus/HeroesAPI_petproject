using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Mounts.Commands.CreateMount;
using Application.Mounts.Commands.DeleteMount;
using Application.Mounts.Commands.UpdateMount;
using Application.Mounts.Queries.GetMount;
using Application.Mounts.Queries.GetMounts;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="admin, user")]
    public class MountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get List of Mounts
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">List was retrieved</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MountsVm>> GetMounts([FromQuery] GetMountsQuery query)
        {
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Get Mount by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Mount was retrieved</response>
        /// <response code="500">Read error message</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         public async Task<ActionResult<MountVm>> GetMountById(int id)
        {
            return await _mediator.Send(new GetMountQuery { MountId = id });

        }

        /// <summary>
        /// Create new Mount ( For admin only )
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="201">Mount was created successfully</response>
        /// <response code="400">Invalid query</response>
        /// <response code="403">You don't have permissions</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> CreateMount(CreateMountCommand command)
        {
            var newMountId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetMountById), new { id = newMountId }, command);
        }

        /// <summary>
        /// Find and edit Mount in database ( For admin only )
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="204">Mount was updated successfully</response>
        /// <response code="400">Invalid query</response>
        /// <response code="403">You don't have permissions</response>
        /// <response code="500">Read error message</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateMount(int id, UpdateMountCommand command)
        {
            if (id != command.MountId)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();

        }

        /// <summary>
        /// Delete Mount from database ( For admin only )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Mount was deleted</response>
        /// <response code="403">You don't have permissions</response>
        /// <response code="500">Read error message</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteHero(int id)
        {
            await _mediator.Send(new DeleteMountCommand { MountId = id });

            return NoContent();
        }
    }
}
