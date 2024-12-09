using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand :IRequest< OperationResult<bool>>
    {
        public int AuthorId { get; }

        public DeleteAuthorCommand(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
