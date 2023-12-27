using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Project : AuditableBaseEntity
    {
        [StringLength(80)]
        public string? Name { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public int OwnerId { get; set; }
        public User? User { get; set; }


        public ICollection<Task>? Tasks { get; set; }

        public ICollection<UserProject>? ProjectUsers { get; set; }
    }
}
