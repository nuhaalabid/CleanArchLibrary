using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, OperationResult<bool>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<DeleteAuthorCommandHandler> _logger;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, ILogger<DeleteAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<OperationResult<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start processing DeleteAuthorCommand for AuthorId: {AuthorId}", request.AuthorId);
 
             var author = await _authorRepository.GetAuthorById(request.AuthorId);
            
            if (author == null)
            {
                _logger.LogWarning("Author with ID {AuthorId} not found.", request.AuthorId);
                return OperationResult<bool>.Failure("Author not found.");
            }

            await _authorRepository.DeleteAuthor(request.AuthorId);
            _logger.LogInformation("Author with ID {AuthorId} deleted successfully.", request.AuthorId);

            return OperationResult<bool>.Success(true);
        }
    }

}
