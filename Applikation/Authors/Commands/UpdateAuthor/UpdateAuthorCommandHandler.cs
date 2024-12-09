using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, ILogger<UpdateAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start updating author with ID: {AuthorId}", request.AuthorId);

            var author = await _authorRepository.GetAuthorById(request.AuthorId);
            if (author == null)
            {
                _logger.LogWarning("Author with ID: {AuthorId} not found.", request.AuthorId);
                return OperationResult<Author>.Failure("Author not found.");
            }

            // Update the author's properties
            author.Name = request.UpdatedAuthor.Name;

            var updatedAuthor = await _authorRepository.UpdateAuthor(author);
            _logger.LogInformation("Successfully updated author with ID: {AuthorId}", request.AuthorId);
            return OperationResult<Author>.Success(updatedAuthor);
        }
    }
}

