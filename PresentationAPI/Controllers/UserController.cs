using Applikation.Dtos;
using Applikation.Users.Commands.AddUser;
using Applikation.Users.Queries.GetAll;
using Applikation.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _mediator.Send(new GetAllUserQuery());

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            var result = await _mediator.Send(new AddUserCommand(newUser));

            if (result == null)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(result);
        }


        [HttpPost]
        [Route ("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginUser)
        {
            try
            {              
                var token = await _mediator.Send(new LoginUserQuery(loginUser));
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}