using Domain.Common;

namespace Domain.Entities
{
    public class Project : AuditableBaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required int ProjectOwnerId { get; set; }


        public ICollection<Task>? Tasks { get; set; }

        public ICollection<UserProject>? ProjectUsers { get; set; }

        public required User User { get; set; }
    }
}
