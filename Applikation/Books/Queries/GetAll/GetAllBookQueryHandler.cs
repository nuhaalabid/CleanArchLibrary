using Applikation.Interfaces.RepositoryInterfaces;
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
        private readonly IBookRepository _bookRepository;

        public GetAllBookQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAllBooks();
        }
    }
}
