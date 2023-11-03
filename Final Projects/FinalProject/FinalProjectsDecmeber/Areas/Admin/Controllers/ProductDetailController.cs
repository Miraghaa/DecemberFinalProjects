using December.Business.ViewModels.AreasViewModels.ProductDetailVMs;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class ProductDetailController : Controller
{
    private readonly AppDbContext _context;

    public ProductDetailController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<ProductDetail> productDetails = _context.productDetails.ToList();
        return View(productDetails);
    }
    public async Task<IActionResult> Details(int id)
    {
        ProductDetail? productDetail = await _context.productDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (productDetail == null) return NotFound();
        return View(productDetail);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDetailCreateVM product)
    {
        if (!ModelState.IsValid) return View(product);
        ProductDetail productDetail = new()
        {
            LongDescription = product.LongDescription,
            ShortDescription = product.ShortDescription
        };
        await _context.productDetails.AddAsync(productDetail);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        ProductDetail? productDetail = await _context.productDetails.FindAsync(id);
        if (productDetail == null) return NotFound();
        return View(productDetail);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        ProductDetail? productDetail = await _context.productDetails.FindAsync(id);
        if (productDetail == null) return NotFound();
        _context.productDetails.Remove(productDetail);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        ProductDetail? productDetail = await _context.productDetails.FindAsync(id);
        if (productDetail == null) return NotFound();
        ProductDetailUploadVM productDetailUpload = new()
        {
            Id = productDetail.Id,
            ShortDescription = productDetail.ShortDescription,
            LongDescription = productDetail.LongDescription
        };
        return View(productDetailUpload);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, ProductDetailUploadVM productDetail)
    {
        if (!ModelState.IsValid) return View(productDetail);
        ProductDetail? productDB = await _context.productDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        productDB.ShortDescription = productDetail.ShortDescription;

        ProductDetail product1 = new()
        {
            Id = productDetail.Id,
            ShortDescription = productDetail.ShortDescription,
            LongDescription = productDetail.LongDescription
        };
        productDB = product1;
        _context.productDetails.Update(productDB);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
