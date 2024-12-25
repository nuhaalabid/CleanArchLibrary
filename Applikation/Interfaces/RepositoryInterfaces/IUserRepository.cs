using Applikation.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Interfaces.RepositoryInterfaces
{
    public  interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserByUsername(string username);
        Task<List<User>> GetAllUsers();
        
    }
}


