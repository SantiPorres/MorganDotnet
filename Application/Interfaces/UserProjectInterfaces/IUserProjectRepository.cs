using Domain.Entities;

namespace Application.Interfaces.UserProjectInterfaces
{
    public interface IUserProjectRepository
    {
        Task<bool> InsertUserProjectRelation(UserProject userProject);
    }
}
