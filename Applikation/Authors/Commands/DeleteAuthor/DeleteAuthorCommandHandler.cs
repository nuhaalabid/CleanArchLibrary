using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.DeleteAuthor
{

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorById(request.AuthorId);

            if (author == null)
                return false;

            await _authorRepository.DeleteAuthor(request.AuthorId);
            return true;
        }
    }
}
