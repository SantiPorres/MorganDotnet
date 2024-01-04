using Application.Interfaces.IRepositories;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        IUserProjectRepository UsersProjects { get; }
        IAssignmentRepository Assignments { get; }
        Task<int> Complete();
    }
}
