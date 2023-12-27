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

        public async Task<Project?> GetProjectById(int projectId)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == projectId);
        }

        public async Task<Project> InsertProject(Project project)
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
