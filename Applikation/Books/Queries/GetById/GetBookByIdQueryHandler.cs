using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Queries.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBookById(request.BookId);
        }
    }
}
