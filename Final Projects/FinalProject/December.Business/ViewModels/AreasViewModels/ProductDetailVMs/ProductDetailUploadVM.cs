using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.ProductDetailVMs;

public class ProductDetailUploadVM
{
    public int Id { get; set; }
    [Required, MaxLength(155), MinLength(15)]
    public string ShortDescription { get; set; }

    [Required, MaxLength(255), MinLength(25)]
    public string LongDescription { get; set; }
}
