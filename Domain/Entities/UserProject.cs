using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserProject(
        int userId,
        int projectId,
        UserRole role
        ) : AuditableBaseEntity
    {
        public int UserId { get; set; } = userId;

        public int ProjectId { get; set; } = projectId;

        public UserRole Role { get; set; } = role;


        public User? User { get; set; }

        public Project? Project { get; set; }
    }
}
