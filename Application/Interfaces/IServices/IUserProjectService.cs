using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IUserProjectService
    {
        Task<UserProjectDTO> CreateRelation(UserProject userProject);

        //Task<bool> VerifyRelation(Guid projectId, Guid userId);
    }
}
