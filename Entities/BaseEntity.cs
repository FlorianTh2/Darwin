namespace Darwin.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{

#nullable disable

    public T Id { get; set; }

#nullable enable

    public DateTime CreatedOn { get; set; }
    public string? CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdaterId { get; set; }
}