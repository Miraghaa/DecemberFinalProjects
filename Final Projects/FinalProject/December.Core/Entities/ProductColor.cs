using December.Core.Entities.Areas;

namespace December.Core.Entities;

public class ProductColor : BaseEntity
{
    public int ColorId { get; set; }

    public Color Color { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }
}
