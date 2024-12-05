using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorById(request.Id);

            if (author == null)
                return null;

            author.Name = request.UpdatedAuthor.Name;

            await _authorRepository.UpdateAuthor(author);
            return author;
        }
    }
}
