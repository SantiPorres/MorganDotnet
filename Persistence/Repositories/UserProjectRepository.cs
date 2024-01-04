using Application.Interfaces.IRepositories;
using Domain.CustomExceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        //public async Task<IEnumerable<UserProject>> GetRelations(Guid? projectId, Guid? userId)
        //{
        //    IQueryable<UserProject> query = _dbContext.UserProjects;
        //    if (projectId is not null)
        //        query = query.Where(x => x.ProjectId == projectId);
        //    if (userId is not null)
        //        query = query.Where(x => x.UserId == userId);
        //    IEnumerable<UserProject> relations = await query.ToListAsync();
        //    return relations;
        //}
    }
}
