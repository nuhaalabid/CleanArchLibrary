using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.AddAuthor(request.NewAuthor);
        }
    }
}
