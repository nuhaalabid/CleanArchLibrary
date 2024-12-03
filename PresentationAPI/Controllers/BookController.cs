using Applikation.Books.Commands.DeleteBook;
using Applikation.Books.Commands.UpdateBook;
using Applikation.Books.Queries.GetAll;
using Applikation.Books.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _mediator.Send(new GetAllBookQuery());

            // Kontrollera om inga böcker hittades
            if (result == null || !result.Any())
            {
                return NotFound("Inga böcker hittades.");
            }
            return Ok(result);
        }


        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));

            if (result == null)
            {
                return NotFound($"Boken med ID {id} hittades inte."); 
            }
            return Ok(result);
        }


        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest("ID i URL och kropp matchar inte.");
            }
            var command = new UpdateBookByIdCommand(updatedBook, id);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound($"Boken med ID {id} hittades inte.");
            }
            return Ok(result); 
        }



        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));

            if (!result)
            {
                return NotFound("Book not found.");
            }
            return Ok("Book deleted successfully.");
        }

    }
}
