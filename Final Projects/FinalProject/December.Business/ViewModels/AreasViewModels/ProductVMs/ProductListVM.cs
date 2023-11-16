using December.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace December.Business.ViewModels.AreasViewModels.ProductVMs;

public class ProductListVM
{
    public ProductListVM()
    {
        Images = new List<Image>();
        ColorName = new List<string>();
        SizeType = new List<string>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Discount { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public string MainImag { get; set; }
    public IFormFile? MainImage { get; set; }
    public string HoverImag { get; set; }
    public IFormFile? HoverImage { get; set; }
    public List<Image> Images { get; set; }
    public List<IFormFile>? Imagess { get; set; }
    public Collection? Collection { get; set; }
    public List<string> ColorName { get; set; }
    public List<string> SizeType { get; set; }
    public List<int>? SizeIds { get; set; }
    public List<int>? ColorIds { get; set; }
}
