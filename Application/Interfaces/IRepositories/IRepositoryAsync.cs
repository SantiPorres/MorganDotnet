using System.Linq.Expressions;

namespace Application.Interfaces.IRepositories
{
    public interface IRepositoryAsync<T> : IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetSeveralAsync(IEnumerable<Guid> ids);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> AddAndGetAsync(T entity);
    }
}
