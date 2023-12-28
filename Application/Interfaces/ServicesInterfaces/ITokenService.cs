using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Interfaces.ServicesInterfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenFromHeaders(HttpContext context);

        Task<JwtSecurityToken> HandleJWT(string token);

        Task<Guid?> GetUserIdFromJwt(HttpContext context);

        Task<string> GenerateJWT(User user);
    }
}
