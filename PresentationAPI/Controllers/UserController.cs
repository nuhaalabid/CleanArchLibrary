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
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                _logger.LogInformation("Försöker hämta alla användare...");

                var result = await _mediator.Send(new GetAllUserQuery());

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("Kunde inte hämta användare: {ErrorMessage}", result.ErrorMessage);
                    return NotFound(new
                    {
                        Message = result.ErrorMessage,
                        Details = result.Message
                    });
                }

                _logger.LogInformation("Hämtade alla användare framgångsrikt.");
                return Ok(new
                {
                    Message = result.Message,
                    Users = result.Data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av användare.");
                return StatusCode(500, new
                {
                    Message = "Ett oväntat fel inträffade. Försök igen senare."
                });
            }
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Ogiltig data skickades vid registrering.");
                    return BadRequest(new
                    {
                        Message = "Ogiltiga data skickades.",
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Försöker registrera användare med användarnamn: {Username}", newUser.Username);

                var result = await _mediator.Send(new AddUserCommand(newUser));

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("Kunde inte registrera användare: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new
                    {
                        Message = result.ErrorMessage,
                        Details = result.Message
                    });
                }

                _logger.LogInformation("Användare {Username} registrerades framgångsrikt.", newUser.Username);
                return CreatedAtAction(nameof(GetAllUser), new
                {
                    Message = result.Message,
                    User = result.Data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid registrering av användare.");
                return StatusCode(500, new
                {
                    Message = "Ett oväntat fel inträffade. Försök igen senare."
                });
            }

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginUser)
        {
            try
            {
                // Skicka login förfrågan till MediatR
                var result = await _mediator.Send(new LoginUserQuery(loginUser));

                if (!result.IsSuccessful)
                {
                    // Returnera 401 Unauthorized om inloggningen misslyckas
                    return Unauthorized(result.ErrorMessage);
                }

                // Returnera token om inloggningen lyckades
                return Ok(new { Token = result.Data });
            }
            catch (Exception ex)
            {
                // Logga det oväntade felet
                _logger.LogError(ex, "Ett fel inträffade vid inloggning av användare {Username}.", loginUser.Username);
                return StatusCode(500, "Ett oväntat fel inträffade. Försök igen senare.");
            }
        }
    }
}