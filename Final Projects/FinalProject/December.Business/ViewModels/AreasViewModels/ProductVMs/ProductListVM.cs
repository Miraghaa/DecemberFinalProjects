using December.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace December.Business.ViewModels.AreasViewModels.ProductVMs;

public class ProductListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Discount { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public IFormFile MainImage { get; set; }
    public string? Images { get; set; }
    public Category Category { get; set; }
    public Collection Collection { get; set; }
    public Brand Brand { get; set; }
}
