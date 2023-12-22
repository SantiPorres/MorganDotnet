using Domain.Interfaces;

namespace Domain.Common
{
    public class AuditableBaseEntity : IAuditableBaseEntity
    {
        public int Id { get; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; }

        public DateTime LastModifiedAt { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
