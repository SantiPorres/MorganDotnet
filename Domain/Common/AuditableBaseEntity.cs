using Domain.Interfaces;

namespace Domain.Common
{
    public class AuditableBaseEntity : IAuditableBaseEntity
    {
        public Guid Id { get; }
        
        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public Guid LastModifiedBy { get; set; }
    }
}
