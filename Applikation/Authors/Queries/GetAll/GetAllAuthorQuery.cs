using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Queries.GetAll
{
    public class GetAllAuthorQuery : IRequest<List<Author>>
    {
    }
}
