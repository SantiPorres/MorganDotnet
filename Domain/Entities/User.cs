using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public required string Email { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }


        public ICollection<Task>? Tasks { get; set; }

        public ICollection<UserProject>? UserProjects { get; set; }
    }
}
