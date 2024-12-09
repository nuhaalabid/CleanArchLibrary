using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Commands.AddAuthor
{
   
        public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, OperationResult<Author>>
        {
            private readonly IAuthorRepository _authorRepository;
            private readonly ILogger<AddAuthorCommandHandler> _logger;

            public AddAuthorCommandHandler(IAuthorRepository authorRepository, ILogger<AddAuthorCommandHandler> logger)
            {
                _authorRepository = authorRepository;
                _logger = logger;
            }

            public async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request?.NewAuthor?.Name))
                {
                    _logger.LogWarning("Invalid input: Author name is required.");
                    return OperationResult<Author>.Failure("Author name is required.");
                }

                try
                {
                    _logger.LogInformation("Adding a new author: {AuthorName}", request.NewAuthor.Name);

                    var addedAuthor = await _authorRepository.AddAuthor(request.NewAuthor);

                    _logger.LogInformation("Author {AuthorName} added successfully.", addedAuthor.Name);
                    return OperationResult<Author>.Success(addedAuthor);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding author: {AuthorName}", request.NewAuthor.Name);
                    return OperationResult<Author>.Failure("An error occurred while adding the author.");
                }
            }
        }
    }

