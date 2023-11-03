using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.SliderVMs;

public class SliderUpdateVM
{
    public int Id { get; set; }
    public string? SliderDescriptionone { get; set; }

    public string? SliderDescription { get; set; } 

    public string? SliderValue { get; set; } 

    public string? SliderIamgeUrl { get; set; }

    public IFormFile? Image { get; set; }


}
