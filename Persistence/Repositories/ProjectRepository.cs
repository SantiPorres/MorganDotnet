using Application.Interfaces.ProjectInterfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project?> GetProjectById(Guid projectId, bool navigateProjectUsers)
        {
            Project? project;
            if (navigateProjectUsers)
            {
                project = await _dbContext.Projects
                    .Include(p => p.ProjectUsers)
                    .SingleOrDefaultAsync(x => x.Id == projectId);
            } else
            {
                project = await _dbContext.Projects
                    .SingleOrDefaultAsync(x => x.Id == projectId);
            }
            return project;
        }

        public async Task<Project> AddAsync(Project project)
        {
            
            var newProject = await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return newProject.Entity;
        }

        public async Task<bool> DeleteProject(Project project)
        {
            _dbContext.Projects.Remove(project);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
