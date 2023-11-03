using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.CategoryVMs;

public class CategoryUploadVM
{
    public int Id { get; set; }

    [Required, MaxLength(30), MinLength(5)]
    public string CategoryName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? CategoryImage { get; set; }
}
