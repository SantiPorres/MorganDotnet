using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IUserProjectService
    {
        Task<bool> CreateRelation(UserProject userProject);

        Task<bool> VerifyRelation(Guid projectId, Guid userId);
    }
}
