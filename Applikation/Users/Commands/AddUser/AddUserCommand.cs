
using Applikation.Dtos;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Commands.AddUser
{
    public class AddUserCommand : IRequest<OperationResult<User>>
    {
        public UserDto NewUser { get; set; }

        public AddUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }
    }
}