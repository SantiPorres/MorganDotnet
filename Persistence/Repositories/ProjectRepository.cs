using Application.Interfaces.IRepositories;
using Domain.CustomExceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ProjectRepository : RepositoryAsync<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dbContext)
            : base(dbContext) 
        {
        }

        public async Task<Project> GetWithNavigationAsync(Guid projectId)
        {
            try
            {
                Project project = await Context.Set<Project>()
                    .Include(project => project.ProjectUsers)
                    .SingleOrDefaultAsync(project => project.Id == projectId)
                    ?? throw new KeyNotFoundException("Project does not exist");
                return project;
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
