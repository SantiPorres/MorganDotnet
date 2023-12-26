using Application.Interfaces;
using Application.Interfaces.Services;
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
            
        }
    }
}
