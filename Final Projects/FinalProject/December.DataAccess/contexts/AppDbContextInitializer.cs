using December.Core.Entities;
using December.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace December.DataAccess.contexts;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AppDbContextInitializer(AppDbContext context,
                                   UserManager<AppUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task Initialize()
    {
        await _context.Database.MigrateAsync();
    }

    public async Task CreateRole()
    {
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            if(!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new() { Name = role.ToString() });
            }
        }
    }
    public async Task CreateAdmin()
    {
        AppUser superadmin = new()
        {
            FirstName = "admin",
            UserName = _configuration["SuperAdmin:Username"],
            Email = _configuration["SuperAdmin:Email"],
        };
        await _userManager.CreateAsync(superadmin, _configuration["SuperAdmin:Password"]);
        await _userManager.AddToRoleAsync(superadmin, Roles.SuperAdmin.ToString());
    }

}
