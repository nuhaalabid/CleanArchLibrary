using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly FakeDatabase _fakeDatabase;

        public DeleteBookCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            // Hitta boken baserat på BookId
            var bookToDelete = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.BookId);

            // Om boken inte finns, returnera false
            if (bookToDelete == null)
            {
                return Task.FromResult(false);
            }

            _fakeDatabase.Books.Remove(bookToDelete);

            // Returnera true om boken togs bort
            return Task.FromResult(true);
        }
    }
}
