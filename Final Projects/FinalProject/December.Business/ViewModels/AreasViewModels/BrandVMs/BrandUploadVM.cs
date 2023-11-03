using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.BrandVMs;

public class BrandUploadVM
{
    public int Id { get; set; }

    [Required, MaxLength(30), MinLength(4)]
    public string BrandName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? BrandImage { get; set; }
}
