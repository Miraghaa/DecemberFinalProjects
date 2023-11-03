using December.Core.Entities.Areas;
using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Image : BaseEntity
{
    [Required]
    public string ImageUrl { get; set; } = null!;
    [Required]
    public bool IsMain { get; set; } = false;
    [Required]
    public bool Hoverimage { get; set; } = false;
    public int ProductId { get; set; }
    public Product Products { get; set; }
}
