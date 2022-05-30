using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Heroes.Commands.CreateHero;
using Application.Heroes.Commands.DeleteHero;
using Application.Heroes.Commands.UpdateHero;
using Application.Heroes.Queries.GetHero;
using Application.Heroes.Queries.GetHeroes;
using Application.Heroes.Commands.HeroAddMount;
using Application.Heroes.Commands.HeroRemoveMount;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user, admin")]
    public class HeroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HeroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get List of Heroes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">List was retrieved</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HeroesVm>> GetHeroes([FromQuery] GetHeroesQuery query)
        {
            return await _mediator.Send(query);
        }
        
        /// <summary>
        /// Get Hero by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Hero was retrieved</response>
        /// <response code="500">Read error message</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HeroVm>> GetHeroById (int id)
        {
            return await _mediator.Send(new GetHeroQuery { HeroId = id});
            
        }

        /// <summary>
        /// Create new Hero 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="201">Hero was created successfully</response>
        /// <response code="400">Invalid query</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create (CreateHeroCommand command)
        {
            var newHeroId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetHeroById), new { id = newHeroId }, command);
        }

        /// <summary>
        /// Find and edit Hero in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="204">Hero was updated successfully</response>
        /// <response code="400">Invalid query</response>
        /// <response code="500">Read error message</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateHero(int id, UpdateHeroCommand command)
        {
            if (id != command.HeroId)
                return BadRequest();

            await _mediator.Send(command);

            return Ok();

        }

        /// <summary>
        /// Delete Hero from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Hero was deleted</response>
        /// <response code="500">Read error message</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteHero(int id)
        {
            await _mediator.Send(new DeleteHeroCommand { HeroId = id });

            return NoContent();
        }

        /// <summary>
        /// Add the Mount to Hero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">Mount was added to the Hero</response>
        /// <response code="400">Invalid query</response>
        /// <response code="500">Read error message</response>
        [HttpPut("mount/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> HeroAddMount(int id, HeroAddMountCommand command)
        {
            if (id != command.HeroId)
                return BadRequest();

            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Remove Hero's mount 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">Mount was removed</response>
        /// <response code="400">Invalid query</response>
        /// <response code="500">Read error message</response>
        [HttpDelete("mount/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> HeroRemoveMount(int id,HeroRemoveMountCommand command)
        {
            if (id != command.HeroId)
                return BadRequest();

            await _mediator.Send(command);

            return Ok();
        }
    }
}
