using December.Core.Entities.Areas;
using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Brand : BaseEntity
{
    [Required]
    public string BrandName { get; set; } = null!;

    [Required]
    public string BrandIamgeUrl { get; set; } = null!;

    [Required]
    public ICollection<Product> Products { get; set; }
}
