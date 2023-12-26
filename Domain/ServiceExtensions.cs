using Application.Options;
using Domain.Options;
using Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceExtensions
    {
        public static void AddDomainCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<ProjectValidator>();

            services.AddValidatorsFromAssemblyContaining<UserValidator>();

            services.AddValidatorsFromAssemblyContaining<UserProjectValidator>();

            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

            services.Configure<PaginationOptions>(options => configuration.GetSection("PaginationSettings").Bind(options));
        }
    }
}
