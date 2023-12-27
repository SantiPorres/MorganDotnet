using Application.Interfaces;
using Application.Interfaces.ProjectInterfaces;
using Application.Interfaces.ServicesInterfaces;
using Application.Interfaces.UserInterfaces;
using Application.Interfaces.UserProjectInterfaces;
using Application.Services;
using Application.Services.ProjectServices;
using Application.Services.UserProjectServices;
using Application.Services.UserServices;
using Application.Validators.AccountValidators;
using Application.Validators.ProjectValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            #region Validators

            services.AddValidatorsFromAssemblyContaining<RegisterUserDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginUserDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateProjectDTOValidator>();

            #endregion

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                }
            );

            #region Services

            services.AddScoped<ITokenService, JwtTokenService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IUserProjectService, UserProjectService>();

            #endregion
        }
    }
}
