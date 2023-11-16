using December.Business.ViewModels.AreasViewModels.ProductVMs;
using December.Core.Entities;
using December.Core.Entities.Areas;
using December.DataAccess.contexts;
using FinalProjectsDecmeberUI.ViewModels.HomeVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Controllers;

public class ShopController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ShopController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    private async Task<HomeVM> GetFilteredHomeVM(string categoryName, int[] brandIds, int[] categoryIds)
    {
        IQueryable<Product> filteredProducts = _context.Products;
        filteredProducts = filteredProducts.Where(p => p.Stock > 0);

        if (!string.IsNullOrEmpty(categoryName))
        {
            filteredProducts = filteredProducts.Where(p => p.Categorys.CategoryName == categoryName);
        }

        if (brandIds != null && brandIds.Length > 0)
        {
            filteredProducts = filteredProducts.Where(p => brandIds.Contains(p.BrandId));
        }

        if (categoryIds != null && categoryIds.Length > 0)
        {
            filteredProducts = filteredProducts.Where(p => categoryIds.Contains(p.CategoryId));
        }

        var vm = new HomeVM
        {
            Products = await filteredProducts
                            .Include(p => p.Brands)
                            .Include(p => p.Categorys)
                            .Include(p => p.Collections)
                            .Include(p => p.Detail)
                            .Include(p => p.Colors)
                            .Include(p => p.Sizes)
                            .Include(p => p.Images)
                            .Take(15)
                            .ToListAsync(),
            Brands = await _context.Brands.ToListAsync(),
            Images = await _context.Images.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
        };

        return vm;
    }

    public async Task<HomeVM> GetCategoryProducts(string categoryName)
    {
        int men = await _context.Products
                                    .Where(p => p.Categorys.CategoryName == "men")
                                    .CountAsync();
        int women = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "women")
                                   .CountAsync();
        int accesories = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "Accesories")
                                   .CountAsync();
        var kids = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "Kids")
                                   .CountAsync();
        var news = await _context.Products
                                   .Where(p => p.Collections.CollectionName == "NewArrivals")
                                   .CountAsync();

        var products = await _context.Products
                            .Include(p => p.Brands)
                            .Include(p => p.Categorys)
                            .Include(p => p.Collections)
                            .Include(p => p.Detail)
                            .Include(p => p.Colors)
                            .Include(p => p.Sizes)
                            .Include(p => p.Images)
                            .Where(p => p.Categorys.CategoryName == categoryName)
                            .Take(12)
                            .ToListAsync();

        HomeVM vm = new()
        {
            Products = products,
            MenProductCount = men,
            WomenProductCount = women,
            AccesoriesProductCount = accesories,
            KidsProductCount = kids,
            NewProductCount = news,
            Brands = await _context.Brands.ToListAsync(),
            Images = await _context.Images.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
        };

        return vm;
    }
    public async Task<IActionResult> Index()
    {
        var men = await _context.Products
                                    .Where(p => p.Categorys.CategoryName == "men")
                                    .CountAsync();
        var women = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "women")
                                   .CountAsync();
        var accesories = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "Accesories")
                                   .CountAsync();
        var jwellery = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "Jwellery")
                                   .CountAsync();
        var kids = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "kids")
                                   .CountAsync();
        var news = await _context.Products
                                   .Where(p => p.Collections.CollectionName == "NewArrivals")
                                   .CountAsync();

        var vm = await GetFilteredHomeVM(null, null, null);
        vm.MenProductCount = men;
        vm.WomenProductCount = women;
        vm.AccesoriesProductCount = accesories;
        vm.KidsProductCount = kids;
        vm.JwelleryProductCount = jwellery;
        vm.NewProductCount = news;

        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int[] brandIds, int[] categoryIds)
    {
        var vm = await GetFilteredHomeVM(null, brandIds, categoryIds);
        return View(vm);
    }

    public async Task<IActionResult> Men()
    {
        HomeVM vm = await GetCategoryProducts("men");
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Men(string categoryName, int[] brandIds, int[] categoryIds)
    {
        var vm = await GetFilteredHomeVM("Men", brandIds, categoryIds);
        return View(vm);
    }
    public async Task<IActionResult> Women()
    {
        HomeVM vm = await GetCategoryProducts("women");
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Women(string categoryName, int[] brandIds, int[] categoryIds)
    {
        var vm = await GetFilteredHomeVM("Women", brandIds, categoryIds);
        return View(vm);
    }
    public async Task<IActionResult> Accesories()
    {
        HomeVM vm = await GetCategoryProducts("Accesories");
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accesories(string categoryName, int[] brandIds, int[] categoryIds)
    {
        var vm = await GetFilteredHomeVM("Accesories", brandIds, categoryIds);
        return View(vm);
    }
    public async Task<IActionResult> Kids()
    {
        HomeVM vm = await GetCategoryProducts("Kids");
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Kids(string categoryName, int[] brandIds, int[] categoryIds)
    {
        var vm = await GetFilteredHomeVM("Kids", brandIds, categoryIds);
        return View(vm);
    }
    public async Task<IActionResult> New()
    {
        int men = await _context.Products
                                  .Where(p => p.Categorys.CategoryName == "men")
                                  .CountAsync();
        int women = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "women")
                                   .CountAsync();
        int accesories = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "Accesories")
                                   .CountAsync();
        var kids = await _context.Products
                                   .Where(p => p.Categorys.CategoryName == "Kids")
                                   .CountAsync();
        var news = await _context.Products
                                   .Where(p => p.Collections.CollectionName == "NewArrivals")
                                   .CountAsync();

        var products = await _context.Products
                                .Include(p => p.Brands)
                                .Include(p => p.Categorys)
                                .Include(p => p.Collections)
                                .Include(p => p.Detail)
                                .Include(p => p.Colors)
                                .Include(p => p.Sizes)
                                .Include(p => p.Images)
                                .Where(p => p.Collections.CollectionName == "NewArrivals")
                                .Take(12)
                                .ToListAsync();

        HomeVM vm = new()
        {
            Products = products,
            MenProductCount = men,
            WomenProductCount = women,
            AccesoriesProductCount = accesories,
            KidsProductCount = kids,
            NewProductCount = news,
            Brands = await _context.Brands.ToListAsync(),
            Images = await _context.Images.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
        };

        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(string categoryName, int[] brandIds, int[] categoryIds)
    {
        var vm = await GetFilteredHomeVM("New", brandIds, categoryIds);
        return View(vm);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products
       .Include(p => p.Brands) 
       .Include(p => p.Categorys) 
       .Include(p => p.Collections) 
       .Include(p => p.Detail) 
       .Include(p => p.Colors)
       .Include(p => p.Sizes) 
       .Include(p => p.Images) 
       .Include(p => p.Review).ThenInclude(r => r.Comment) 
       .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)return RedirectToAction("Error", "Home");
        return View(product);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Detail(int id,int rate, string text)
    {
        var product = await _context.Products
       .Include(p => p.Brands) 
       .Include(p => p.Categorys) 
       .Include(p => p.Collections) 
       .Include(p => p.Detail) 
       .Include(p => p.Colors) 
       .Include(p => p.Sizes) 
       .Include(p => p.Images) 
       .Include(p => p.Review).ThenInclude(r=>r.Comment).OrderByDescending(r => r.Id) 
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
        if (product == null) return RedirectToAction("Error", "Home");
        await  _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return View(product);
    }
}
