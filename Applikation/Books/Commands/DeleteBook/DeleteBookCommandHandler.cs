using Applikation.Interfaces.RepositoryInterfaces;

using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult<bool>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(IBookRepository bookRepository, ILogger<DeleteBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public async Task<OperationResult<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _bookRepository.GetBookById(request.BookId);

                if (book == null)
                {
                    _logger.LogWarning("Boken med ID {Id} kunde inte hittas.", request.BookId);
                    return OperationResult<bool>.Failure($"Boken med ID {request.BookId} kunde inte hittas.");
                }
                await _bookRepository.DeleteBook(request.BookId);

                _logger.LogInformation("Boken med ID {Id} raderades framgångsrikt.", request.BookId);
                return OperationResult<bool>.Success(true, "Boken raderades framgångsrikt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid borttagning av boken med ID {Id}.", request.BookId);
                return OperationResult<bool>.Failure("Ett oväntat fel inträffade. Försök igen senare.");
            }
        }

    }
}
