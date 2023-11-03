using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Size : BaseEntity
{
    [Required]
    public string Type { get; set; } = null!;

    [Required]
    public ICollection<ProductSize> Products { get; set; }
}
