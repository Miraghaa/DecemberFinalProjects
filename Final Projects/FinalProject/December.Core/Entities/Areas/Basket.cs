using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities.Areas;

public class Basket : BaseEntity
{
    public string IamgeUrl { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; } 
    public int Count { get; set; }
    public string UserIds { get; set; }
    public AppUser User { get; set; }

}
