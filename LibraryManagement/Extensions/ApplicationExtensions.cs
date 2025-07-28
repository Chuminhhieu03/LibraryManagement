using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace LibraryManagement.API.Extensions
{
    public static class ApplicationExtensions
    {
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
