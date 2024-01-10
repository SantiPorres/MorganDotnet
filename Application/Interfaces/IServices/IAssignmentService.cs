using Application.DTOs.AssignmentDTOs;

namespace Application.Interfaces.IServices
{
    public interface IAssignmentService
    {
        Task<AssignmentDTO> GetAssignmentById(Guid assignmentId);
        Task<AssignmentDTO> AddAssignment(CreateAssignmentDTO body, Guid projectId);
    }
}
