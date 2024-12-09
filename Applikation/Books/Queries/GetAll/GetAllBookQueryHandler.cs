using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Queries.GetAll
{

    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<GetAllBookQueryHandler> _logger; 

        public GetAllBookQueryHandler(IBookRepository bookRepository, ILogger<GetAllBookQueryHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger; 
        }

        public async Task<OperationResult<List<Book>>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Hämtar alla böcker."); 

                var books = await _bookRepository.GetAllBooks();

                if (books == null || books.Count == 0)
                {
                    _logger.LogWarning("Inga böcker hittades."); 
                    return OperationResult<List<Book>>.Failure("Inga böcker hittades.");
                }

                _logger.LogInformation("Böcker hämtades framgångsrikt. Antal böcker: {Count}", books.Count); 

                return OperationResult<List<Book>>.Success(books, "Böcker hämtades framgångsrikt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av böcker."); 
                return OperationResult<List<Book>>.Failure($"Ett fel inträffade: {ex.Message}");
            }
        }
    }
}
    

