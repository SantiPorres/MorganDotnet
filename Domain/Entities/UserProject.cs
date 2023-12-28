using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserProject : AuditableBaseEntity
    {
        [Required]
        public required Guid UserId { get; set; }
        [Required]
        public required Guid ProjectId { get; set; }
        [Required]
        public required UserRole Role { get; set; }
    }
}
