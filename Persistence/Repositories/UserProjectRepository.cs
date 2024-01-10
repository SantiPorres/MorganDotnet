using Application.Interfaces.IRepositories;
using Domain.CustomExceptions;
using Domain.Entities;
using Persistence.Contexts;
namespace Persistence.Repositories
{
    public class UserProjectRepository : RepositoryAsync<UserProject>, IUserProjectRepository
    {
        public UserProjectRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext ?? throw new DataAccessException(); }
        }
    }
}
