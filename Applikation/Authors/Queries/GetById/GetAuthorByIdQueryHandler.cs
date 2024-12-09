using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Queries.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery,OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<GetAuthorByIdQueryHandler> _logger;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, ILogger<GetAuthorByIdQueryHandler> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<OperationResult<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching author with ID {AuthorId}", request.AuthorId);

            try
            {
                var author = await _authorRepository.GetAuthorById(request.AuthorId);

                if (author == null)
                {
                    _logger.LogWarning("Author with ID {AuthorId} not found", request.AuthorId);
                    return OperationResult<Author>.Failure("Author not found.");
                }

                _logger.LogInformation("Successfully fetched author with ID {AuthorId}", request.AuthorId);
                return OperationResult<Author>.Success(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching author with ID {AuthorId}", request.AuthorId);
                return OperationResult<Author>.Failure("An error occurred while fetching the author.");
            }
        }
    }
}