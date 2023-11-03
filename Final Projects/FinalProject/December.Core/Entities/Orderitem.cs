using December.Core.Entities.Areas;
using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Orderitem : BaseEntity
{
    [Required]
    public int Count { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public string Size { get; set; }

    [Required]
    public string Color { get; set; }

    public int ProductId { get; set; }

    public Product Products { get; set; }

    public int OrderId { get; set; }

    public Order Orders { get; set; }
}
