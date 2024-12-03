using Applikation.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Users.Queries.Login
{
    public class LoginUserQuery :IRequest<string>
    {
        public LoginUserQuery(UserDto loginUser)
        {
            LoginUser = loginUser;           
        }
        public UserDto LoginUser { get; set; }



    }
}
