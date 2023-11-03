using December.Business.Exceptions;
using December.Business.Services.Implementations;
using December.Business.Services.Interfaces;
using December.Business.ViewModels.AreasViewModels.BrandVMs;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class BrandController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _web;
    private readonly IFileService _fileservice;

    public BrandController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _web = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index(int Id)
    {
        Brand brand = _context.Brands.FirstOrDefault(x => x.Id == Id);
        List<Brand> brands = _context.Brands.ToList();
        return View(brands);
    }
    public async Task<IActionResult> Details(int id)
    {
        Brand? brand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (brand == null) return NotFound();
        return View(brand);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandCreateVM brand)
    {
        if (!ModelState.IsValid) return View(brand);
        string filename = string.Empty;
        try
        {
            Brand brand1 = new()
            {
                BrandName = brand.BrandName
            };
            filename = await _fileservice.UploadFile(brand.Image, _web.WebRootPath, 300, "assets", "photos", "Brand");
            brand1.BrandIamgeUrl = filename;
            await _context.Brands.AddAsync(brand1);
            await _context.SaveChangesAsync();
        }
        catch (FileSizeException ex)
        {
            ModelState.AddModelError("BrandImage", ex.Message);
            return View(brand);
        }
        catch (FileTypeException ex)
        {
            ModelState.AddModelError("BrandImage", ex.Message);
            return View(brand);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(brand);
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound();
        return View(brand);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound();
        string fileroot = Path.Combine(_web.WebRootPath, brand.BrandIamgeUrl);
        if (System.IO.File.Exists(fileroot))
        {
            System.IO.File.Delete(fileroot);
        }
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult> Update(int id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound();
        BrandUploadVM brandUpload = new()
        {
            Id = brand.Id,
            BrandName = brand.BrandName,
            BrandImage = brand.BrandIamgeUrl,
        };
        return View(brandUpload);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, BrandUploadVM brand)
    {
        if (!ModelState.IsValid) return View(brand);
        Brand? branddb = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (branddb == null) return NotFound();
        if (brand.Image != null)
        {
            try
            {
                string filename = await _fileservice.UploadFile(brand.Image, _web.WebRootPath, 300, "assets", "photos", "Brand");
                _fileservice.RemoveFile(_web.WebRootPath, branddb.BrandIamgeUrl);
                Brand brandd = new()
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    BrandIamgeUrl = brand.BrandImage
                };
                branddb = brandd;
                branddb.BrandIamgeUrl = filename;
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(brand);
            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(brand);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(brand);
            }
        }
        else
        {
            brand.BrandImage = branddb.BrandIamgeUrl;
            Brand brandd = new()
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                BrandIamgeUrl = brand.BrandImage
            };
            branddb = brandd;
        }
        //return Content(_context.Entry(sliderdb).State.ToString());
        _context.Brands.Update(branddb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
