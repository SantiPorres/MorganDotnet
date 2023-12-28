namespace Domain.Interfaces;

public interface IAuditableBaseEntity
{
    Guid Id { get; }

    DateTime CreatedAt { get; set; }

    Guid CreatedBy { get; }

    DateTime LastModifiedAt { get; set; }

    Guid LastModifiedBy { get; set; }
}
