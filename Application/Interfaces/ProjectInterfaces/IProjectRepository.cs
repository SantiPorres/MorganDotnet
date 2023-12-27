using Domain.Entities;

namespace Application.Interfaces.ProjectInterfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectById(int projectId);

        Task<Project> InsertProject(Project project);

        Task<bool> DeleteProject(Project project);
    }
}
