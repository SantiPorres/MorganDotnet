using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Projects = new ProjectRepository(_context);
            UsersProjects = new UserProjectRepository(_context);
            Assignments = new AssignmentRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IUserProjectRepository UsersProjects { get; private set; }
        public IAssignmentRepository Assignments { get; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
