using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities.Areas;

public class Product : BaseEntity
{
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public int Totalrating { get; set;}
    [Required]
    public int? Stock {get; set;}
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal Discount { get; set; }
    [Required] 
    public DateTime UpdatedTime { get; set; }
    [Required]
    public DateTime DeletedTime { get; set; }
    [Required]
    public DateTime CreatedTime { get; set; }
    // Relation

    public int BrandId { get; set; }
    public Brand Brands { get; set; }
    public int CategoryId { get; set;}
    public Category Categorys { get; set; }
    public int CollectionId { get; set; }
    public Collection Collections { get; set; }
    public int DetailId { get; set; }
    public ProductDetail Detail { get; set; }
    public List<ProductColor> Colors { get; set; }
    public List<ProductSize> Sizes { get; set; }
    public List<Image> Images { get; set; }
    public List<Review> Review { get; set; }
    public Product()
    {
        Images = new();
        Colors = new();
        Sizes = new();
        Review = new();
    }

}
