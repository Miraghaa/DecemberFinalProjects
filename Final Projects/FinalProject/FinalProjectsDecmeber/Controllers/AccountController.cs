using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectsDecmeberUI.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
//public class AccountController : Controller
//{
//    private readonly UserManager<AppUser> _userManager; // ApplicationUser modeli kullanılıyor
//    private readonly AppDbContext _context; // Veritabanı bağlantısı için ApplicationDbContext

//    public AccountController(UserManager<AppUser> userManager, AppDbContext context)
//    {
//        _userManager = userManager;
//        _context = context;
//    }

//    [Authorize]
//    public IActionResult Index()
//    {
//        // Kullanıcıyı veritabanından alın (örneğin, Entity Framework kullanarak)
//        var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

//        if (user != null)
//        {
//            string firstName = user.FirstName;
//            string lastName = user.LastName;

//            // firstName ve lastName gibi kişisel bilgileri modelinize ekleyin

//            return View(user); // Kullanıcı modelinizi görüntüleme sayfasına aktarabilirsiniz
//        }

//        return View(); // Kullanıcı bulunamazsa boş bir sayfa veya hata sayfasına yönlendirme yapabilirsiniz
//    }
//}