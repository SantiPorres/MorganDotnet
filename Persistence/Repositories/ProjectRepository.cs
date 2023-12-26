using Application.Interfaces.ProjectInterfaces;
using Domain.CustomExceptions;
using Domain.Entities;
using Microsoft.Data.SqlClient;
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

        public async Task<Project> InsertProject(Project project)
        {
            try
            {
                var newProject = await _dbContext.Projects.AddAsync(project);
                await _dbContext.SaveChangesAsync();
                return newProject.Entity;
            }
            catch (Exception ex) when (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                throw new DataAccessException($"DbUpdateException or DbUpdateConcurrencyException: {ex.Message}");
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"SqlException: {ex.Message}", ex.Errors);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An error occurred: {ex.Message}");
            }
        }
    }
}
