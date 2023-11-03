using Microsoft.AspNetCore.Http;

namespace December.Business.ViewModels.AreasViewModels.CollectionVMs;

public class CollectionCreateVM
{
    public string CollectionName { get; set; } = null!;

    public IFormFile Image { get; set; }
}
