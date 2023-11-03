using December.Core.Entities.Areas;
using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Collection : BaseEntity
{
    [Required]
    public string CollectionName { get; set; }

    [Required]
    public string CollectionImageUrl { get; set; }

    [Required]
    public ICollection<Product> Products { get; set; }
}
