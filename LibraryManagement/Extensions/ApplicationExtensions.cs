using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace LibraryManagement.API.Extensions
{
    /// <summary>
    /// Provides extension methods for application-specific service configurations.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Adds application-specific services to the specified IServiceCollection instance.
        /// Includes configuration for AutoMapper, MediatR, and FluentValidation.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the application services will be added.</param>
        /// <returns>The updated IServiceCollection with the registered application services.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(typeof(LibraryManagement.Application.Handlers.Books.CreateBookCommandHandler).Assembly);
            }
            );

            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
