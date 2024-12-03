using Infrastructure.Database;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly FakeDatabase _database;

        public UpdateAuthorCommandHandler(FakeDatabase database)
        {
            _database = database;
        }
        public Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = _database.Authors.FirstOrDefault(a => a.Id == request.Id);

            if (authorToUpdate != null)
            {
                authorToUpdate.Name = request.UpdatedAuthor.Name;
            }

            return Task.FromResult(authorToUpdate);
        }

    }
}
