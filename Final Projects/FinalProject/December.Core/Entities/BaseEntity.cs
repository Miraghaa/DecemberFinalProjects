namespace December.Core.Entities;

public class BaseEntity
{
    public virtual int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

}
