using Domain.Entities;

namespace Application.Interfaces.UserProjectInterfaces
{
    public interface IUserProjectService
    {
        Task<bool> CreateRelation(UserProject userProject);
    }
}
