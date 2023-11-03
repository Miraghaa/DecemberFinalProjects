using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.SliderVMs;

public class SliderCreateVM
{
    [Required, MaxLength(30), MinLength(5)]
    public string SliderDescriptionone { get; set; } = null!;

    [Required, MaxLength(100), MinLength(5)]
    public string SliderDescription { get; set; } = null!;

    [Required]
    public string SliderValue { get; set; } = null!;

    [Required]
    public IFormFile SliderIamgeUrl { get; set; } = null!;
}
