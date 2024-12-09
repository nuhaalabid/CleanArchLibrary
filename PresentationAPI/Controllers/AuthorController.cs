
using Applikation.Authors.Commands.AddAuthor;
using Applikation.Authors.Commands.DeleteAuthor;
using Applikation.Authors.Commands.UpdateAuthor;
using Applikation.Authors.Queries.GetAll;
using Applikation.Authors.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        // GET: api/<AuthorController>
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                _logger.LogInformation("Fetching all authors.");

                var result = await _mediator.Send(new GetAllAuthorQuery());

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("Failed to fetch authors: {Message}", result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Successfully fetched authors.");
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all authors.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }


        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            _logger.LogInformation("Received request to fetch author with ID {AuthorId}", id);

            var result = await _mediator.Send(new GetAuthorByIdQuery(id));

            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Failed to fetch author with ID {AuthorId}: {ErrorMessage}", id, result.Message);
                return NotFound(result.Message);
            }

            _logger.LogInformation("Successfully fetched author with ID {AuthorId}", id);
            return Ok(result.Data);
        }


        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author newAuthor)
        {
            _logger.LogInformation("Start processing AddAuthor request.");

            if (newAuthor == null || string.IsNullOrWhiteSpace(newAuthor.Name))
            {
                _logger.LogWarning("Invalid Author data received.");
                return BadRequest("Author data is invalid.");
            }

            try
            {
                var command = new AddAuthorCommand(newAuthor);
                _logger.LogInformation("Sending AddAuthorCommand to MediatR.");

                var result = await _mediator.Send(command);

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("Failed to add author. Reason: {Reason}", result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Author {AuthorName} added successfully with ID: {AuthorId}.", result.Data.Name, result.Data.Id);
                return CreatedAtAction(nameof(GetAuthorById), new { id = result.Data.Id }, result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding author: {AuthorName}.", newAuthor.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            _logger.LogInformation("Startar uppdatering av författare med ID: {AuthorId}", id);

            if (updatedAuthor == null || string.IsNullOrWhiteSpace(updatedAuthor.Name))
            {
                _logger.LogWarning("Uppdateringsdata för författare är ogiltig. AuthorId: {AuthorId}", id);
                return BadRequest("Ogiltiga uppgifter för författare.");
            }

            var command = new UpdateAuthorCommand(id, updatedAuthor);
            var result = await _mediator.Send(command);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Uppdateringen av författaren med ID: {AuthorId} misslyckades. Meddelande: {Message}", id, result.Message);
                return NotFound(result.Message);
            }

            _logger.LogInformation("Författaren med ID: {AuthorId} har uppdaterats framgångsrikt.", id);
            return Ok(result.Data); 
        }



        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            _logger.LogInformation("Start processing DeleteAuthor for AuthorId: {AuthorId}", id);

            var command = new DeleteAuthorCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Failed to delete AuthorId: {AuthorId}. Reason: {Reason}", id, result.Message);
                return NotFound(result.Message);
            }

            _logger.LogInformation("Successfully deleted AuthorId: {AuthorId}", id);
            return NoContent(); 
        }
    }
}

