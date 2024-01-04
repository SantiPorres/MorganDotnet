using Domain.Enums;

namespace Application.DTOs
{
    public class UserProjectDTO
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public required UserRole Role { get; set; }
    }
}
