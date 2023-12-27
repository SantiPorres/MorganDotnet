using Domain.Entities;

namespace Application.Interfaces.UserProjectInterfaces
{
    public interface IUserProjectRepository
    {
        Task<UserProject> InsertUserProjectRelation(UserProject userProject);
    }
}
