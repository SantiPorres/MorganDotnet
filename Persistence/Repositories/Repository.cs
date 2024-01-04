using Application.Interfaces.IRepositories;
using Domain.CustomExceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IAuditableBaseEntity
    {
        protected readonly DbContext Context;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public T Get(Guid id)
        {
            try
            {
                return Context.Set<T>()
                    .Single(x => x.Id == id)
                    ?? throw new KeyNotFoundException($"{typeof(T)} does not exist");
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return Context.Set<T>().ToList();
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return Context.Set<T>().Where(predicate);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }


        public void Add(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            try
            {
                Context.Set<T>().AddRange(entities);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public T AddAndGet(T entity)
        {
            try
            {
                T newEntity = (Context.Set<T>().Add(entity)).Entity;
                return newEntity;
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }


        public void Remove(T entity)
        {
            try
            {
                Context.Set<T>().Remove(entity);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
             try
            {
                Context.Set<T>().RemoveRange(entities);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }
    }
}
