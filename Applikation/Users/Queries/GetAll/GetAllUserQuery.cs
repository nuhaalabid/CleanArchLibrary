using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Queries.GetAll
{
    public class GetAllUserQuery : IRequest<OperationResult<List<User>>>
    {
    }
}
