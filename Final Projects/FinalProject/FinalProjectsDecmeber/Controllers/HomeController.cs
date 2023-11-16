using December.Core.Entities.Areas;
using December.DataAccess.contexts;
using FinalProjectsDecmeberUI.ViewModels.HomeVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        HomeVM vm = new()
        {
            MenProductCount = await _context.Products.Where(p => p.Categorys.CategoryName == "men").CountAsync(),
            AccesoriesProductCount = await _context.Products.Where(p => p.Categorys.CategoryName == "Accesories").CountAsync(),
            JwelleryProductCount = await _context.Products.Where(p => p.Categorys.CategoryName == "Jwellery").CountAsync(),
            Brands = await _context.Brands.ToListAsync(),
            Images = await _context.Images.ToListAsync(),
            Sliders = await _context.Sliders.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
            Products = await _context.Products
                        .Include(p => p.Brands)
                        .Include(p => p.Categorys)
                        .Include(p => p.Collections)
                        .Include(p => p.Detail)
                        .Include(p => p.Colors)
                        .Include(p => p.Sizes)
                        .Include(p => p.Images)
                        .ToListAsync(),
        };
        ViewBag.Collections = _context.Collections.ToList();
        return View(vm);
    }
    public IActionResult Error()
    {
        return View();
    }
}
