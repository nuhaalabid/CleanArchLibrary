using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Applikation.Books.Commands.AddBook.AddBookCommad;

namespace Applikation.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = request.NewBook;
            return await _bookRepository.AddBook(newBook);
        }

    }
}

