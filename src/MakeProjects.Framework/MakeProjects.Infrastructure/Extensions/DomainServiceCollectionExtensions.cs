using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods to register domain services in the DI container.
    /// </summary>
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // Example: register base domain services
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
