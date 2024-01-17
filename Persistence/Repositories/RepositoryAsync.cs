using Application.Interfaces.IRepositories;
using Domain.CustomExceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class RepositoryAsync<T> : Repository<T>, IRepositoryAsync<T> where T : class, IAuditableBaseEntity
    {
        public RepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<T> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<T>()
                    .FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new KeyNotFoundException("Not found");
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Context.Set<T>().ToListAsync();
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await Context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public async Task<IEnumerable<T>> GetSeveralAsync(IEnumerable<Guid> ids)
        {
            try
            {
                return await Context.Set<T>()
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }


        public async Task AddAsync(T entity)
        {
            try
            {
                await Context.Set<T>().AddAsync(entity);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await Context.Set<T>().AddRangeAsync(entities);
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }

        public async Task<T> AddAndGetAsync(T entity)
        {
            try
            {
                var newEntity = await Context.Set<T>().AddAsync(entity);
                return newEntity.Entity;
            }
            catch (Exception ex) { throw new DataAccessException(ex.Message); }
        }
    }
}
