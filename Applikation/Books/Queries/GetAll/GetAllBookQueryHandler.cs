using Infrastructure.Database;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Queries.GetAll
{
    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, List<Book>>
    {
        private readonly FakeDatabase _database;

        public GetAllBookQueryHandler(FakeDatabase database)
        {
            _database = database;
        }
        public Task<List<Book>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            var books = _database.Books;
            return Task.FromResult(books);
        }


    }
}
