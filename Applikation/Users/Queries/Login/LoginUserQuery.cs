using Applikation.Dtos;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Queries.Login
{
    public class LoginUserQuery :IRequest<OperationResult<string>>
    {
        public LoginUserQuery(UserDto loginUser)
        {
            LoginUser = loginUser;           
        }
        public UserDto LoginUser { get; set; }



    }
}
