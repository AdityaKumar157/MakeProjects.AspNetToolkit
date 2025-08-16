using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.AspNetToolkit.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods to register domain services in the DI container.
    /// </summary>
    public static class DomainServiceCollectionExtensions
    {
        /// <summary>
        /// Adds domain services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // Example: register base domain services
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
