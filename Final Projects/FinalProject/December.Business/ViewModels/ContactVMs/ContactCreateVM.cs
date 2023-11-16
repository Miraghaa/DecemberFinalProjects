using System.ComponentModel.DataAnnotations;

namespace December.Business.ViewModels.ContactVMs;

public class ContactCreateVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public int Phone { get; set; }
    [Required]
    public string Message { get; set; }
}
