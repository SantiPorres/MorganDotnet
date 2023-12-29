using Application.DTOs.ProjectDTOs;
using Application.Filters;
using Domain.CustomEntities;

namespace Application.Interfaces.ProjectInterfaces
{
    public interface IProjectService
    {
        Task<PagedList<ProjectDTO>> GetProjectsByUserId(PaginationQueryParameters filters, Guid userId);

        Task<ProjectNavigationDTO> GetProjectById(Guid projectId);

        Task<ProjectDTO> CreateProject(Guid userId, CreateProjectDTO body);
    }
}
