using Application.DTOs.ProjectDTOs;
using Application.Filters;
using Domain.CustomEntities;

namespace Application.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<PagedList<ProjectDTO>> GetAllProjectsByUserId(PaginationQueryParameters filters, Guid userId);

        Task<ProjectDTO> GetProjectById(Guid projectId, bool? navigable = true);

        Task<ProjectDTO> CreateProject(Guid userId, CreateProjectDTO body);
    }
}
