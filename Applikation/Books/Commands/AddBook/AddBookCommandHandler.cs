using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Applikation.Books.Commands.AddBook.AddBookCommad;

namespace Applikation.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<AddBookCommandHandler> _logger; 

        public AddBookCommandHandler(IBookRepository bookRepository, ILogger<AddBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger; 
        }
        public async Task<OperationResult<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = request.NewBook;

            try
            {
                var addedBook = await _bookRepository.AddBook(newBook); 
                _logger.LogInformation("Boken '{Title}' har lagts till med ID {Id}.", newBook.Title, addedBook.Id);
                return OperationResult<Book>.Success(addedBook); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fel inträffade vid tillägg av boken '{Title}'.", newBook.Title);
                return OperationResult<Book>.Failure("Ett fel inträffade vid tillägg av boken."); 
            }
        }
    }
}

