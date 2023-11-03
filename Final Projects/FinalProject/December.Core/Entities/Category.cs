using December.Core.Entities.Areas;
using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Category : BaseEntity
{
    [Required]
    public string CategoryName { get; set; } = null!;

    [Required] 
    public string CategoryImageUrl { get; set; } = null!;

    [Required]
    public ICollection<Product> Products { get; set; }

}
