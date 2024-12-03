using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.AddAuthor
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor;
        }
        public Author NewAuthor { get; }
    }
}


