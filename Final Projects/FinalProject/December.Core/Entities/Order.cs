using December.Core.Entities.Areas;
using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Order : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Postcode { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string UserIds { get; set; }
    public AppUser User { get; set; }

}
