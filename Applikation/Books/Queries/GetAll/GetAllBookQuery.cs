using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Queries.GetAll
{
    public class GetAllBookQuery : IRequest<OperationResult<List<Book>>>
    {

    }
}
