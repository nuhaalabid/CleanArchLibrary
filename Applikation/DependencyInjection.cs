using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applikation.Users.Queries.Login.Helpers;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Applikation
{
        public static class DependencyInjection
        {
            public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)
            {
                var assembly = typeof(DependencyInjection).Assembly;

                services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            // Lägg till Realdatabase med anslutningssträngen
            services.AddDbContext<Realdatabase>(options =>
                options.UseSqlServer(connectionString));


            //services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<TokenHelper>();

                return services;
            }

        }
    }

