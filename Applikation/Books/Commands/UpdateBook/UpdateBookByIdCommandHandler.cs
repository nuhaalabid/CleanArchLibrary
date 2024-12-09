using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<UpdateBookByIdCommandHandler> _logger;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository, ILogger<UpdateBookByIdCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public async Task<OperationResult<Book>> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _bookRepository.GetBookById(request.Id);

                if (book == null)
                {
                    _logger.LogWarning("Boken med ID {Id} kunde inte hittas.", request.Id);
                    return OperationResult<Book>.Failure($"Boken med ID {request.Id} kunde inte hittas.");
                }
                book.Title = request.UpdatedBook.Title;
                book.Description = request.UpdatedBook.Description;
                book.Author = request.UpdatedBook.Author;

                // Spara ändringarna
                var updatedBook = await _bookRepository.UpdateBook(book);

                _logger.LogInformation("Boken med ID {Id} uppdaterades framgångsrikt.", request.Id);
                return OperationResult<Book>.Success(updatedBook, "Boken uppdaterades framgångsrikt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid uppdatering av boken med ID {Id}.", request.Id);
                return OperationResult<Book>.Failure("Ett oväntat fel inträffade. Försök igen senare.");
            }
        }
    }
}
    


