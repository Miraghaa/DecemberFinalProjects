using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities.Areas;

public class Slider : BaseEntity
{
    [Required]
    public string SliderDescriptionone { get; set; } = null!;

    [Required]
    public string SliderDescription { get; set; } = null!;

    [Required]
    public string SliderValue { get; set; } = null!;

    [Required]
    public string SliderIamgeUrl { get; set; } = null!;


}
