using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Color : BaseEntity
{
    [Required]
    public string ColorName { get; set; } = null!;

    [Required]
    public string Image { get; set; } = null!;

    [Required]
    public List<ProductColor> Products { get; set; }
    public Color()
    {
        Products = new();
    }
}
