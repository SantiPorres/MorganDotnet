using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJWT(User user);
    }
}
