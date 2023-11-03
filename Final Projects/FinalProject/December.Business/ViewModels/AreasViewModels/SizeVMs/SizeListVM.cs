using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.SizeVMs;

public class SizeListVM
{
    [Required]
    public string SizeName { get; set; } = null!;
}
