using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.ProductVMs;

public class ProductCreateVM 
{
    public string ProductName { get; set; }
    public int Discount { get; set; }
    public int Stock { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public IFormFile MainImage { get; set; }
    public IFormFile HoverImage { get; set; }
    public List<IFormFile> Images { get; set; }
    public List<int> ColorIds { get; set; }
    public List<int> SizeIds { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int CollectionId { get; set; }
    public int DetailId { get; set; }
}
