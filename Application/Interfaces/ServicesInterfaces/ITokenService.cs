using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Interfaces.ServicesInterfaces
{
    public interface ITokenService
    {
        string GetTokenFromHeaders(HttpContext context);

        JwtSecurityToken HandleJWT(string token);

        int GetUserIdFromJwt(HttpContext context);

        Task<string> GenerateJWT(User user);
    }
}
