using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Project : AuditableBaseEntity
    {
        [Required]
        [AllowedValues(typeof(string))]
        [StringLength(80)]
        public required string Name { get; set; }
        [AllowNull]
        [AllowedValues(typeof(string))]
        [StringLength(300)]
        public string? Description { get; set; }

        public ICollection<UserProject>? ProjectUsers { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
