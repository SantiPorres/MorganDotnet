using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Assignment : AuditableBaseEntity
    {
        [Required]
        [AllowedValues(typeof(string))]
        [StringLength(80)]
        public required string Title { get; set; }
        [AllowNull]
        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public required Guid ProjectId { get; set; }
        [AllowNull]
        public Guid? UserId { get; set; }
    }
}
