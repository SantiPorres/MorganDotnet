#region Usings

// Application
using Application.Interfaces.IServices;


// Domain
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


// External libraries
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

// Internal libraries
using System.Security.Claims;
using System.Text;

#endregion

namespace Application.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public JwtTokenService(IConfiguration configuration, IDateTimeService dateTime)
        {
            _configuration = configuration;
            _dateTimeService = dateTime;
        }

        public async Task<string> GetTokenFromHeaders(HttpContext context)
        {
            await System.Threading.Tasks.Task.Delay(0);
            string? authorizationHeader = context.Request.Headers.Authorization;
            if (authorizationHeader == null)
                throw new UnauthorizedAccessException();
            string token = authorizationHeader.Replace("Bearer ", "");
            return token;
        }

        public async Task<JwtSecurityToken> HandleJWT(string token)
        {
            await System.Threading.Tasks.Task.Delay(0);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var handledJWT = handler.ReadJwtToken(token);
            return handledJWT;
        }

        public async Task<Guid?> GetUserIdFromJwt(HttpContext context)
        {
            string token = await GetTokenFromHeaders(context);
            JwtSecurityToken jwt = await HandleJWT(token);
            Guid? userId = Guid.Parse(jwt.Subject);
            return userId;
        }

        public async Task<string> GenerateJWT(User user)
        {
            await System.Threading.Tasks.Task.Delay(0);
            var secretKey = _configuration["JwtSettings:SecretKey"] ?? throw new Exception();
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(
                symmetricSecurityKey, 
                SecurityAlgorithms.HmacSha256
            );
            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            var payload = new JwtPayload(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                _dateTimeService.NowUTC,
                _dateTimeService.NowUTC.AddMinutes(10)
            );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
