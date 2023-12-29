using Application.Interfaces.UserProjectInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var newUserProject = await _dbContext.UserProjects.AddAsync(userProject);
            await _dbContext.SaveChangesAsync();
            return newUserProject.Entity;
        }

        public async Task<IEnumerable<UserProject>> GetRelations(Guid? projectId, Guid? userId)
        {
            IQueryable<UserProject> query = _dbContext.UserProjects;
            if (projectId is not null)
                query = query.Where(x => x.ProjectId == projectId);
            if (userId is not null)
                query = query.Where(x => x.UserId == userId);
            IEnumerable<UserProject> relations = await query.ToListAsync();
            return relations;
        }
    }
}
