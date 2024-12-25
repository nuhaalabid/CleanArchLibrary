using Applikation.Interfaces.RepositoryInterfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{ 
        public class UserRepository :IUserRepository 
        {
            private readonly Realdatabase _context;

            public UserRepository(Realdatabase context)
            {
                _context = context;
            }

            public async Task<User> AddUser(User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            public async Task<User> GetUserByUsername(string username)
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }

            public async Task<List<User>> GetAllUsers()
            {
                return await _context.Users.ToListAsync();
            }


        }
    }



