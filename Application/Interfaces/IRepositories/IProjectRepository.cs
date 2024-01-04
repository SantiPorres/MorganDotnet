using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IProjectRepository : IRepositoryAsync<Project>
    {
        Task<Project> GetWithNavigationAsync(Guid projectId);
    }
}
