using Application.DTOs.ProjectDTOs;

namespace Application.Interfaces.ProjectInterfaces
{
    public interface IProjectService
    {
        //Task<PagedResponse<ProjectDTO>> GetUserProjects(int user_id);

        Task<ProjectDTO> CreateProject(int user_id, CreateProjectDTO body);
    }
}
