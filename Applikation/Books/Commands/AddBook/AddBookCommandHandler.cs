using Infrastructure.Database;
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
        private readonly FakeDatabase _database;

        public AddBookCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = request.NewBook;

            _database.Books.Add(newBook);

    }
}
