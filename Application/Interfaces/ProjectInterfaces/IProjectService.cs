using Application.DTOs.ProjectDTOs;

namespace Application.Interfaces.ProjectInterfaces
{
    public interface IProjectService
    {
        Task<ProjectNavigationDTO> GetProjectById(Guid projectId);

        Task<ProjectDTO> CreateProject(Guid userId, CreateProjectDTO body);
    }
}
