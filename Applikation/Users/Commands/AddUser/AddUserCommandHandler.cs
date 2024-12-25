using Applikation.Dtos;
using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IUserRepository userRepository, ILogger<AddUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Kontrollera om användarnamnet redan finns
                var existingUser = await _userRepository.GetUserByUsername(request.NewUser.Username);
                if (existingUser != null)
                {
                    _logger.LogWarning("Användarnamn {Username} finns redan.", request.NewUser.Username);
                    return OperationResult<User>.Failure(
                        $"Användarnamnet '{request.NewUser.Username}' är redan upptaget.",
                        "Add user operation failed");
                }

                // Konvertera UserDto till User
                var userToAdd = new User(0, request.NewUser.Username, request.NewUser.Password);

                // Lägg till den nya användaren
                var addedUser = await _userRepository.AddUser(userToAdd);

                // Logga och returnera framgångsresultatet
                _logger.LogInformation("Användare {Username} har lagts till framgångsrikt.", addedUser.Username);
                return OperationResult<User>.Success(addedUser, "Användare har lagts till framgångsrikt.");
            }
            catch (Exception ex)
            {
                // Logga felet och returnera ett misslyckat resultat
                _logger.LogError(ex, "Ett fel inträffade när användaren skulle läggas till.");
                return OperationResult<User>.Failure(
                    "Ett oväntat fel inträffade. Försök igen senare.",
                    "Add user operation failed");
            }
        }
    }
}
