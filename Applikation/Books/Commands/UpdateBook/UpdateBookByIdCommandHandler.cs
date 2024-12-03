using Infrastructure.Database;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly FakeDatabase _database;

        public UpdateBookByIdCommandHandler(FakeDatabase database)
        {
            _database = database;
        }
        public Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            var book = _database.Books.FirstOrDefault(b => b.Id == request.Id);
            if (book == null)
            {
                return Task.FromResult<Book>(null); 
            }

            //Uppdatera bokens egenskaper
            book.Title = request.UpdatedBook.Title;
            book.Description = request.UpdatedBook.Description;
            book.Author = request.UpdatedBook.Author;

            return Task.FromResult(book); 
        }
    }
}
