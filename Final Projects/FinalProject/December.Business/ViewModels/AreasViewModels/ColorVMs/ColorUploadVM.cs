using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.ColorVMs;

public class ColorUploadVM
{
    public int Id { get; set; }

    [Required]
    public string ColorName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? ColorImage { get; set; }
}
