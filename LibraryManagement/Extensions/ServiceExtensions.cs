using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and register services for dependency injection
    /// within the library management application.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures and registers database-related services including DbContext, repositories,
        /// and unit of work in the service collection for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to register the services into.</param>
        /// <param name="configuration">The application configuration object to retrieve connection strings and settings.</param>
        /// <returns>The same service collection to allow for method chaining.</returns>
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add HttpContextAccessor for audit fields
            services.AddHttpContextAccessor();

            // Register DbContext with SQL Server
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("LibraryManagement.Infrastructure")
                )
            );

            // Register repositories and unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
