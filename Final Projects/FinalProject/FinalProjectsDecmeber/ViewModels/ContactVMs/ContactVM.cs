using December.Core.Entities;
using December.Core.Entities.Areas;

namespace FinalProjectsDecmeberUI.ViewModels.ContactVMs;

public class ContactVM
{
    public AppUser AppUser { get; set; }
    public List<Adress> Adresses { get; set; }
    public Adress Adress { get; set; }
    public List<Basket> Baskets { get; set; }
    public decimal TotalPrice { get; set; }

}
