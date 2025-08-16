using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.AspNetToolkit.Infrastructure.Extensions
{
    /// <summary>
    /// Provides a single entry point to register all infrastructure components.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all infrastructure services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMakeProjectsInfrastructure(this IServiceCollection services)
        {
            services
                .AddRepositories()
                .AddDomainServices();

            return services;
        }
    }
}
