using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetOneById(int id);

        Task<T> Insert(T entity);
    }
}
