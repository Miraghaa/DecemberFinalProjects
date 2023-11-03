using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class ProductDetail : BaseEntity
{
    [MaxLength(150), MinLength(30)]
    public string? ShortDescription { get; set; }

    [MaxLength(250), MinLength(50)]
    public string? LongDescription { get; set; }
}
