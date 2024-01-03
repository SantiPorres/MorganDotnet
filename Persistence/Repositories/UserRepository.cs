using Application.Interfaces.UserInterfaces;
using Domain.CustomExceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<User> GetWithNavigationAsync(Guid userId)
        {
            try
            {
                User user = await Context.Set<User>()
                    .Include(user => user.UserProjects)
                    .SingleOrDefaultAsync(user => user.Id == userId)
                    ?? throw new KeyNotFoundException("User does not exist");
                return user;
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception) { throw new DataAccessException(); }
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext ?? throw new DataAccessException(); }
        }
    }
}
