using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.AreasViewModels.CollectionVMs;

public class CollectionUploadVM
{
    public int Id { get; set; }

    [Required, MaxLength(70), MinLength(3)]
    public string CollectionName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? CollectionImage { get; set; }
}
