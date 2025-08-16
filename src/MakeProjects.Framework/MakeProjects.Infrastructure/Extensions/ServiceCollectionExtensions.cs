using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Infrastructure.Extensions
{
    /// <summary>
    /// Provides a single entry point to register all infrastructure components.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMakeProjectsInfrastructure(this IServiceCollection services)
        {
            services
                .AddRepositories()
                .AddDomainServices();

            return services;
        }
    }
}
