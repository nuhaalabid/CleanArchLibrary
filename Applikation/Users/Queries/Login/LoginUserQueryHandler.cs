using Applikation.Users.Queries.Login.Helpers;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Queries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly FakeDatabase _database;
        private readonly TokenHelper _tokenHelper;

        public LoginUserQueryHandler(FakeDatabase database, TokenHelper tokenHelper)
        {
            _database = database;
            _tokenHelper = tokenHelper;
        }

        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _database.Users.FirstOrDefault(u =>
            u.Username == request.LoginUser.Username && u.Password == request.LoginUser.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = _tokenHelper.GenerateJwtToken(user); 

            return Task.FromResult(token);
        }
    }
}
