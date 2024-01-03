using Domain.Entities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
        Task<User> GetWithNavigationAsync(Guid userId);
    }
}
