using Applikation.Interfaces.RepositoryInterfaces;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Interfaces.RepositoryInterfaces
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Realdatabase>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<IUserRepository, UserRepository>();





            return services;
        }
    }
}
