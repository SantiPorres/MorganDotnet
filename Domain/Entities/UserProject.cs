using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserProject : AuditableBaseEntity
    {
        public required int UserId { get; set; }

        public required int ProjectId { get; set; }

        public required UserRole Role { get; set; }


        public required User User { get; set; }

        public required Project Project { get; set; }
    }
}
