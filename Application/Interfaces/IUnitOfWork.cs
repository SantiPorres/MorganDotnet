using Application.Interfaces.UserInterfaces;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        Task<int> Complete();
    }
}
