using Applikation.Interfaces.RepositoryInterfaces;
using Applikation.Users.Queries.Login.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Queries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository; 
        private readonly TokenHelper _tokenHelper;
        private readonly ILogger<LoginUserQueryHandler> _logger; 

        public LoginUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper, ILogger<LoginUserQueryHandler> logger)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _logger = logger; 
        }

        public async Task<OperationResult<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(request.LoginUser.Username);

            if (user == null || user.Password != request.LoginUser.Password)
            {
                _logger.LogWarning("Inloggning misslyckades för användare {Username}.", request.LoginUser.Username);
                return OperationResult<string>.Failure("Ogiltigt användarnamn eller lösenord.");
            }

            // Generera JWT-token
            var token = _tokenHelper.GenerateJwtToken(user);
            _logger.LogInformation("Inloggning lyckades för användare {Username}.", request.LoginUser.Username);

            return OperationResult<string>.Success(token, "Inloggning lyckades.");
        }
    }
}

