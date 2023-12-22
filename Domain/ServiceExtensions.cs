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

            services.Configure<PaginationOptions>(options => configuration.GetSection("PaginationSettings").Bind(options));
        }
    }
}
