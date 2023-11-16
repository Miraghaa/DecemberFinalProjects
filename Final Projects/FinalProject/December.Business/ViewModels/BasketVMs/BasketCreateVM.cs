using December.Core.Entities.Areas;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.BasketVMs;
public class BasketCreateVM
{
    public int Id { get; set; }
    public string IamgeUrl { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Count { get; set; }

}
