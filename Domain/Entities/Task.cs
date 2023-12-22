using Domain.Common;

namespace Domain.Entities
{
    public class Task : AuditableBaseEntity
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public required int ProjectId { get; set; }

        public int? UserId { get; set; }


        public required Project Project { get; set; }

        public User? User { get; set; }
    }
}
