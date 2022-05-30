using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Jobs.Commands.CreateJob;
using Application.Jobs.Commands.DeleteJob;
using Application.Jobs.Commands.UpdateJob;
using Application.Jobs.Queries.GetJob;
using Application.Jobs.Queries.GetJobs;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin, user")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get List of Jobs
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">List was retrieved</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<JobsVm>> GetJobs([FromQuery] GetJobsQuery query)
        {
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Get Job by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Job was retrieved</response>
        /// <response code="500">Read error message</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JobVm>> GetJobById(int id)
        {
            return await _mediator.Send(new GetJobQuery { JobId = id });

        }

        /// <summary>
        /// Create new Job ( For admin only )
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="201">Job was created successfully</response>
        /// <response code="400">Invalid query</response>
        /// <response code="403">You don't have permissions</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> CreateJob(CreateJobCommand command)
        {
            var newJobId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetJobById), new { id = newJobId }, command);
        }

        /// <summary>
        /// Find and edit Job in database ( For admin only )
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="204">Job  was updated successfully</response>
        /// <response code="400">Invalid query</response>
        /// <response code="403">You don't have permissions</response>
        /// <response code="500">Read error message</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateJob(int id, UpdateJobCommand command)
        {
            if (id != command.JobId)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();

        }

        /// <summary>
        /// Delete Job from database ( For admin only )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Job was deleted</response>
        /// <response code="403">You don't have permissions</response>
        /// <response code="500">Read error message</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteJob(int id)
        {
            await _mediator.Send(new DeleteJobCommand { JobId = id });

            return NoContent();
        }
    }
}
