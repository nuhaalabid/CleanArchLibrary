using Infrastructure.Database;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Queries.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly FakeDatabase _database;

        public GetAuthorByIdQueryHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            // Hämta författaren baserat på ID
            var author = _database.Authors.FirstOrDefault(a => a.Id == request.AuthorId);

            return Task.FromResult(author);
        }
    }
}