using Domain.Entities;

namespace Application.Interfaces.UserProjectInterfaces
{
    public interface IUserProjectRepository
    {
        Task<UserProject> InsertUserProjectRelation(UserProject userProject);

        Task<IEnumerable<UserProject>> GetRelations(Guid? projectId, Guid? userId);
    }
}
