using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Author>
    {
        public UpdateAuthorCommand(Author updatedAuthor, int id)
        {
            UpdatedAuthor = updatedAuthor;
            Id = id;
        }

        public UpdateAuthorCommand(int existingAuthorId, Author updatedAuthor)
        {
            UpdatedAuthor = updatedAuthor;
        }

        public Author UpdatedAuthor { get; }

        public int Id { get; }
    }
}
