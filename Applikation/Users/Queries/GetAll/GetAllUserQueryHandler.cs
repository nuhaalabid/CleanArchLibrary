using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IUserRepository userRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult<List<User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämtar alla användare
                var users = await _userRepository.GetAllUsers();

                // Kontrollera om listan är tom
                if (users == null || users.Count == 0)
                {
                    _logger.LogInformation("Inga användare hittades i systemet.");
                    return OperationResult<List<User>>.Failure("Inga användare hittades.");
                }

                // Returnerar lyckad operation med användarlistan
                _logger.LogInformation("{Count} användare hittades.", users.Count);
                return OperationResult<List<User>>.Success(users, "Användare hämtades framgångsrikt.");
            }
            catch (Exception ex)
            {
                // Hantera fel och logga
                _logger.LogError(ex, "Ett fel inträffade när användarna skulle hämtas.");
                return OperationResult<List<User>>.Failure("Ett oväntat fel inträffade. Försök igen senare.");
            }
        }
    }
}

