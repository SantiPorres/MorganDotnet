using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetOneById(int id);

        Task<User?> GetOneByEmail(string email);

        Task<User> Insert(User user);
    }
}
