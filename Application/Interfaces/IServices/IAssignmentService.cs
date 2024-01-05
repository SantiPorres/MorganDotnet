using Application.DTOs.AssignmentDTOs;

namespace Application.Interfaces.IServices
{
    public interface IAssignmentService
    {
        Task<AssignmentDTO> AddAssignment(CreateAssignmentDTO body);
    }
}
