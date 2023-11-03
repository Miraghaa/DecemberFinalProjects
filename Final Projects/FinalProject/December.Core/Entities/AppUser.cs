using Microsoft.AspNetCore.Identity;

namespace December.Core.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
}
