using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applikation.Users.Queries.Login.Helpers;

namespace Applikation
{
        public static class DependencyInjection
        {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

                services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));


            //services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<TokenHelper>();

                return services;
            }

        }
    }

