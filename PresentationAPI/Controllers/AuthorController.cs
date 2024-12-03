
using Applikation.Authors.Commands.AddAuthor;
using Applikation.Authors.Commands.DeleteAuthor;
using Applikation.Authors.Commands.UpdateAuthor;
using Applikation.Authors.Queries.GetAll;
using Applikation.Authors.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<AuthorController>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _mediator.Send(new GetAllAuthorQuery());

            return Ok(result);
        }


        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author newAuthor)
        {
            try
            {  
                if (newAuthor == null || string.IsNullOrWhiteSpace(newAuthor.Name))
                {
                    return BadRequest(new { Message = "Författarens namn är obligatoriskt." });
                }
                
                var addedAuthor = await _mediator.Send(new AddAuthorCommand(newAuthor));

                if (addedAuthor == null)
                {
                    return StatusCode(500, new { Message = "Ett internt serverfel uppstod vid tillägg av författaren." });
                }
                return CreatedAtAction(nameof(GetAuthorById), new { id = addedAuthor.Id }, addedAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ett oväntat fel inträffade.", Error = ex.Message });
            }
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            var command = new UpdateAuthorCommand(updatedAuthor, id);

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound(new { message = "Author not found" });
            }

            return Ok(result);
        }



        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAuthorCommand(id);
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new { message = "Author deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Author not found." });
            }
        }
    }
}
