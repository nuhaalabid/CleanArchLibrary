using Infrastructure.Database;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly FakeDatabase _database;

        public AddAuthorCommandHandler(FakeDatabase database)
        {
            _database = database;
        }
        public Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthor = request.NewAuthor;

            _database.Authors.Add(newAuthor);

            return Task.FromResult(newAuthor);
        }

    }
}
