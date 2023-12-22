using Application.Interfaces;
using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
        }
    }
}
