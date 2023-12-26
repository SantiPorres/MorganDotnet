using Application.DTOs.ProjectDTOs;
using Application.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ProjectInterfaces
{
    public interface IProjectService
    {
        //Task<PagedResponse<ProjectDTO>> GetUserProjects(int user_id);

        Task<Response<ProjectDTO>> CreateProject(int user_id, CreateProjectDTO body);
    }
}
