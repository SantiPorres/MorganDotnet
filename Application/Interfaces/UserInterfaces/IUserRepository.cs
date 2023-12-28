using Domain.Entities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers(bool navigateUserProjects);

        Task<User?> GetOneById(Guid userId, bool navigateUserProjects);

        Task<User?> GetOneByEmail(string email);

        Task<User> Insert(User user);
    }
}
