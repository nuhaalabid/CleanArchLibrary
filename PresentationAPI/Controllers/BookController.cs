using Applikation.Books.Commands.DeleteBook;
using Applikation.Books.Commands.UpdateBook;
using Applikation.Books.Queries.GetAll;
using Applikation.Books.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using static Applikation.Books.Commands.AddBook.AddBookCommad;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                _logger.LogInformation("Försöker hämta alla böcker.");
                var query = new GetAllBookQuery();
                var result = await _mediator.Send(query);

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning(result.ErrorMessage);
                    return NotFound(result.ErrorMessage);
                }

                _logger.LogInformation(result.Message);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av böcker.");
                return StatusCode(500, "Ett oväntat fel inträffade.");
            }
        }


         // GET api/<BookController>/5
         [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                _logger.LogInformation("Försöker hämta boken med ID {BookId}.", id);
                var query = new GetBookByIdQuery(id);
                var result = await _mediator.Send(query);

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning(result.ErrorMessage);
                    return NotFound(result.ErrorMessage);
                }

                _logger.LogInformation(result.Message);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av boken med ID {BookId}.", id);
                return StatusCode(500, "Ett oväntat fel inträffade.");
            }
        }


        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book newBook)
        {
            _logger.LogInformation("AddBook endpoint called with Title: {Title}", newBook.Title);

            // Modellvalidering
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddBook failed validation for Title: {Title}. Validation errors: {Errors}",
                    newBook.Title,
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return BadRequest(ModelState); // Returnera valideringsfel
            }
            try
            {
                var result = await _mediator.Send(new AddBookCommand(newBook));

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("AddBook failed for Title: {Title}. Reason: {Reason}",
                        newBook.Title, result.ErrorMessage);

                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }

                _logger.LogInformation("AddBook succeeded for Title: {Title}, ID: {Id}", newBook.Title, result.Data.Id);
                return CreatedAtAction(nameof(GetBookById), new { id = result.Data.Id }, result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the book with Title: {Title}", newBook.Title);
                return StatusCode(500, "An unexpected error occurred while adding the book.");
            }
        }



        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            _logger.LogInformation("UpdateBook endpoint called for ID: {Id}", id);

            // Kontrollera om modellen är giltig
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("UpdateBook validation failed for ID: {Id}. Errors: {Errors}",
                    id,
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                // Returnera valideringsfel till klienten
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _mediator.Send(new UpdateBookByIdCommand(updatedBook, id));

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("UpdateBook failed for ID: {Id}. Reason: {Reason}", id, result.ErrorMessage);
                    return NotFound(new { message = result.Message, error = result.ErrorMessage });
                }

                _logger.LogInformation("UpdateBook succeeded for ID: {Id}", id);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "An error occurred while updating the book with ID: {Id}", id);
               return StatusCode(500, "An unexpected error occurred while updating the book.");
            }

        }



        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation("DeleteBook endpoint called with ID: {Id}", id);

            try
            {
                var result = await _mediator.Send(new DeleteBookCommand(id));

                if (!result.IsSuccessful)
                {
                    _logger.LogWarning("DeleteBook failed for ID: {Id}. Reason: {Reason}", id, result.ErrorMessage);
                    return NotFound(new { message = result.Message, error = result.ErrorMessage });
                }

                _logger.LogInformation("DeleteBook succeeded for ID: {Id}", id);
                return Ok(new { message = result.Message, success = result.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book with ID: {Id}", id);
                return StatusCode(500, "An unexpected error occurred while deleting the book.");
            }

        }

    }
}
