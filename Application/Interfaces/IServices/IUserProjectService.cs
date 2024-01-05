using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IUserProjectService
    {
        Task<UserProjectDTO> CreateRelation(UserProject userProject);

        Task<IEnumerable<UserProjectDTO>> GetAllRelations(Guid projectId, Guid userId);
    }
}
