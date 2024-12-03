using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly FakeDatabase _database;

        public DeleteAuthorCommandHandler(FakeDatabase database)
        {
            _database = database;
        }
        public Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToDelete = _database.Authors.FirstOrDefault(a => a.Id == request.AuthorId);

            // Om författaren inte finns, returnera false
            if (authorToDelete == null)
            {
                return Task.FromResult(false);
            }
            _database.Authors.Remove(authorToDelete);

            // Returnera true om författaren togs bort framgångsrikt
            return Task.FromResult(true);
        }
    }
}
