using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Queries.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<GetBookByIdQueryHandler> _logger; 

        public GetBookByIdQueryHandler(IBookRepository bookRepository, ILogger<GetBookByIdQueryHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Hämtar bok med ID: {BookId}", request.BookId); 

                var book = await _bookRepository.GetBookById(request.BookId);

                if (book == null)
                {
                    _logger.LogWarning("Boken med ID: {BookId} hittades inte.", request.BookId); 
                    return OperationResult<Book>.Failure("Boken hittades inte.");
                }

                _logger.LogInformation("Boken med ID: {BookId} hämtades framgångsrikt.", request.BookId); 
                return OperationResult<Book>.Success(book, "Boken hämtades framgångsrikt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av boken med ID: {BookId}.", request.BookId); 
                return OperationResult<Book>.Failure($"Ett fel inträffade: {ex.Message}");
            }
        }
    }
}


