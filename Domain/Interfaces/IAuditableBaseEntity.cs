namespace Domain.Interfaces;

public interface IAuditableBaseEntity
{
    int Id { get; }

    DateTime CreatedAt { get; set; }

    string? CreatedBy { get; }

    DateTime LastModifiedAt { get; set; }

    string? LastModifiedBy { get; set; }
}
