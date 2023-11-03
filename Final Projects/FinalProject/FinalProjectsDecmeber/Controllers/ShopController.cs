using December.Business.ViewModels.AreasViewModels.ProductVMs;
using December.Core.Entities;
using December.Core.Entities.Areas;
using December.DataAccess.contexts;
using FinalProjectsDecmeberUI.ViewModels.HomeVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Controllers;

public class ShopController : Controller
{
    private readonly AppDbContext _context;

    public ShopController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        HomeVM vm = new()
        {
            Products = await _context.Products
                            .Include(p => p.Brands)
                            .Include(p => p.Categorys)
                            .Include(p => p.Collections)
                            .Include(p => p.Detail)
                            .Include(p => p.Colors)
                            .Include(p => p.Sizes)
                            .Include(p => p.Orders)
                            .Include(p => p.Images)
                            .Take(12)
                            .ToListAsync(),
            Brands = await _context.Brands.ToListAsync(),
            Images = await _context.Images.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
        };

        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int[] brandIds, int[] categoryIds)
    {
        HomeVM vm = new()
        {
            Brands = await _context.Brands.ToListAsync(),
            Images = await _context.Images.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
        };

        IQueryable<Product> filteredProducts = _context.Products;

        if (brandIds != null && brandIds.Length > 0)
        {
            filteredProducts = filteredProducts.Where(p => brandIds.Contains(p.BrandId));
        }

        if (categoryIds != null && categoryIds.Length > 0)
        {
            filteredProducts = filteredProducts.Where(p => categoryIds.Contains(p.CategoryId));
        }

        vm.Products = await filteredProducts
            .Include(p => p.Brands)
            .Include(p => p.Categorys)
            .Include(p => p.Collections)
            .Include(p => p.Detail)
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .Include(p => p.Orders)
            .Include(p => p.Images)
            .Take(12)
            .ToListAsync();

        return View(vm);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products
       .Include(p => p.Brands) // Brand'ı include et
       .Include(p => p.Categorys) // Category'yi include et
       .Include(p => p.Collections) // Collection'ı include et
       .Include(p => p.Detail) // ProductDetail'ı include et
       .Include(p => p.Colors) // ProductColor listesini include et
       .Include(p => p.Sizes) // ProductSize listesini include et
       .Include(p => p.Orders) // Orderitem listesini include et
       .Include(p => p.Images) // Image listesini include et
       .Include(p => p.Review).ThenInclude(r => r.Comment) // Review listesini include et
       .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Detail(int id,int rate, string text)
    {
        var product = await _context.Products
       .Include(p => p.Brands) // Brand'ı include et
       .Include(p => p.Categorys) // Category'yi include et
       .Include(p => p.Collections) // Collection'ı include et
       .Include(p => p.Detail) // ProductDetail'ı include et
       .Include(p => p.Colors) // ProductColor listesini include et
       .Include(p => p.Sizes) // ProductSize listesini include et
       .Include(p => p.Orders) // Orderitem listesini include et
       .Include(p => p.Images) // Image listesini include et
       .Include(p => p.Review).ThenInclude(r=>r.Comment) // Review listesini include et
       .FirstOrDefaultAsync(p => p.Id == id);
        Comment comment = new()
        {
            Comments = text
        };
        Review review = new()
        {
            Product = product,
            Comment = comment
        };
        review.Rating += rate;
        if (product == null)
        {
            return NotFound(); 
        }

        await  _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return View(product);
    }
}
