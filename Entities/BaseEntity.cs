namespace hello_asp_identity.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdaterId { get; set; }
}