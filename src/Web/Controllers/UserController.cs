using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Users.Commands.UserRegister;
using Application.Users.Commands.UserLogin;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register a new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Register is done</response>
        /// <response code="400">Read error message</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("register")]
        public async Task<ActionResult> Register (UserRegisterCommand request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        /// <summary>
        /// Login and get access token in response
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Login was success</response>
        /// <response code="400">Read error message</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("login")]

        public async Task<ActionResult> Login (UserLoginCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
