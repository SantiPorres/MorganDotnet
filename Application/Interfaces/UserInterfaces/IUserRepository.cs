using Domain.Entities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetOneById(int id);

        Task<User?> GetOneByEmail(string email);

        Task<User> Insert(User user);
    }
}
