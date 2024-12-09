//using Applikation.Dtos;
//using Applikation.Interfaces.RepositoryInterfaces;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Applikation.Users.Commands.AddUser
//{
//    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
//    {
//        private readonly FakeDatabase _database;

//        public AddUserCommandHandler(FakeDatabase database)
//        {
//            _database = database;
//        }
//        public Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
//        {
//            var newUser = request.NewUser;

//            if (_database.Users.Any(u => u.Username == newUser.Username))
//            {
//                return Task.FromResult<UserDto>(null);
//            }
//            var user = new Models.User(_database.Users.Count + 1, newUser.Username, newUser.Password);
//            _database.Users.Add(user);

//            return Task.FromResult(new UserDto { Username = user.Username, Password = user.Password });
//        }

//    }
//}
       