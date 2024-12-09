﻿using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<OperationResult<Author>>
    {
        public UpdateAuthorCommand(int authorId, Author updatedAuthor)
        {
            AuthorId = authorId;
            UpdatedAuthor = updatedAuthor;
        }
        public Author UpdatedAuthor { get; }
        public int AuthorId { get; }
    }
}

