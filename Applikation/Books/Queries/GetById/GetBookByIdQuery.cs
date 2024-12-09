using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Queries.GetById
{
    public class GetBookByIdQuery : IRequest<OperationResult<Book>>
     
    {
        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; }
    }

}
