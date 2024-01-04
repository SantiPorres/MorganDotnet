using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        [Required]
        [AllowedValues(typeof(string))]
        [EmailAddress]
        [StringLength(130)]
        public required string Email { get; set; }
        [Required]
        [AllowedValues(typeof(string))]
        [StringLength(50)]
        public required string Username { get; set; }
        [Required]
        [AllowedValues(typeof(string))]
        [StringLength(100)]
        public required string Password { get; set; }

        public ICollection<UserProject>? UserProjects { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }

        public new virtual Guid? CreatedBy { get; } = null;
        public new virtual Guid? LastModifiedBy { get; set; } = null;
    }
}
