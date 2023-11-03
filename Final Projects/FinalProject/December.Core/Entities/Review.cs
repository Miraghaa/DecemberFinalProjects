using December.Core.Entities.Areas;

namespace December.Core.Entities;

public class Review : BaseEntity
{
    public int Rating { get; set; }

    public int CommentId { get; set; }

    public Comment Comment { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }
}
