using December.Business.Mappers;
using December.Business.Services.Implementations;
using December.Business.Services.Interfaces;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(SliderProfile).Assembly);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;

    options.User.RequireUniqueEmail = true;

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = @"/Auth/Login";
});
builder.Services.AddScoped<IFileService, FileService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    var instance = provider.GetRequiredService<AppDbContextInitializer>();
    await instance.Initialize();
    await instance.CreateRole();
    await instance.CreateAdmin();
}
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}"
);
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//    name: "productDetail",
//    pattern: "Admin/Product/Delete/{id}",
//    defaults: new { controller = "Product", action = "Delete" }
//  );

//});


app.Run();
