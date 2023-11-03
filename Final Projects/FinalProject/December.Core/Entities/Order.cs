using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Order : BaseEntity
{
    [Required]
    public decimal TotalPrice { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public ICollection<Orderitem> Products { get; set; }
}
