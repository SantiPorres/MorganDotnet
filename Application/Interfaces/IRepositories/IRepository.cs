using System.Linq.Expressions;

namespace Application.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetSeveral(IEnumerable<Guid> ids);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        T AddAndGet(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
