using Infrastructure.Database;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Queries.GetAll
{
    public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, List<Author>>
    {
        private readonly FakeDatabase _database;

        public GetAllAuthorQueryHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<List<Author>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_database.Authors);
        }
    }
}
