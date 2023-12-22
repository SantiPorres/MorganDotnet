using Domain.Common;

namespace Domain.Entities
{
    public class Project : AuditableBaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }


        public ICollection<Task>? Tasks { get; set; }

        public required ICollection<UserProject> ProjectUsers { get; set; }
    }
}
