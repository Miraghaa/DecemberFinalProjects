using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.CategoryVMs;

public class CategoryCretaeVM
{
    [Required, MaxLength(50), MinLength(3)]
    public string CategoryName { get; set; }

    public IFormFile CategoryImage { get; set; }
}

