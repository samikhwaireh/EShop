namespace EShop.Domain.Entities.Common;

public interface IAuditableEntity<TId>
{
    TId Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}
