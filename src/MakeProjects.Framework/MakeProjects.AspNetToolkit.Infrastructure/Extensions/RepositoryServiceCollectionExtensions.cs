using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeProjects.AspNetToolkit.Abstractions.Repositories;
using MakeProjects.AspNetToolkit.Infrastructure.Repositories;

namespace MakeProjects.AspNetToolkit.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods to register repositories in the DI container.
    /// </summary>
    public static class RepositoryServiceCollectionExtensions
    {
        /// <summary>
        /// Adds repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Register generic repository
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

            // Register specific repositories if any
            // services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
