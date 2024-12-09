
using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Queries.GetAll
{
    public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<GetAllAuthorQueryHandler> _logger;

        public GetAllAuthorQueryHandler(IAuthorRepository authorRepository, ILogger<GetAllAuthorQueryHandler> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllAuthorQuery.");
            try
            {
                var authors = await _authorRepository.GetAllAuthor();
                if (authors == null || !authors.Any())
                {
                    _logger.LogWarning("No authors found.");
                    return OperationResult<List<Author>>.Failure("No authors found.");
                }

                _logger.LogInformation("Successfully retrieved authors.");
                return OperationResult<List<Author>>.Success(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving authors.");
                return OperationResult<List<Author>>.Failure("An error occurred.");
            }
        }
    }
    }

