using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
        Task<User> GetWithNavigationAsync(Guid userId);
    }
}
