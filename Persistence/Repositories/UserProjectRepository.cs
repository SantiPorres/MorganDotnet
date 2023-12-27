using Application.Interfaces.UserProjectInterfaces;
using Domain.Entities;
using Persistence.Contexts;
namespace Persistence.Repositories
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserProject> InsertUserProjectRelation(UserProject userProject)
        {
            var newUserProject = await _dbContext.AddAsync(userProject);
            await _dbContext.SaveChangesAsync();
            return newUserProject.Entity;
        }
    }
}
