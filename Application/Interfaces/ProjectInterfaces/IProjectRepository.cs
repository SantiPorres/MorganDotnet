using Domain.Entities;

namespace Application.Interfaces.ProjectInterfaces
{
    public interface IProjectRepository
    {
        //Task<IEnumerable<Project>> GetAllProjectsByUser();

        //Task<Project?> GetOneById(int id);

        Task<Project> InsertProject(Project project);
    }
}
