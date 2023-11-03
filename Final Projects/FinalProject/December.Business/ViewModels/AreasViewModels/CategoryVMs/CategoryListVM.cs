using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.CategoryVMs;

public class CategoryListVM
{
    public string CategoryName { get; set; }

    public IFormFile Image { get; set; }
}
