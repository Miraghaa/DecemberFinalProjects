using December.Business.Exceptions;
using December.Business.Services.Interfaces;
using December.Business.ViewModels.AreasViewModels.BrandVMs;
using December.Business.ViewModels.AreasViewModels.CategoryVMs;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _web;
    private readonly IFileService _fileservice;

    public CategoryController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _web = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index(int id)
    {
        Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
        List<Category> categories = _context.Categories.ToList();
        return View(categories);
    }
    public ActionResult Detail(int id)
    {
        Category category = _context.Categories.FirstOrDefault(category => category.Id == id);
        if (category == null) return NotFound();
        return View(category);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCretaeVM category)
    {
        if(!ModelState.IsValid) return View(category);
        string filename = string.Empty;
        try
        {
            Category categorys = new()
            {
                CategoryName = category.CategoryName
            };
            filename = await _fileservice.UploadFile(category.CategoryImage, _web.WebRootPath, 300, "assets", "photos", "Category");
            categorys.CategoryImageUrl = filename;
            await _context.Categories.AddAsync(categorys);
            await _context.SaveChangesAsync();
        }
        catch (FileSizeException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(category);
        }
        catch (FileTypeException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(category);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(category);
        }
        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult> Delete(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult> Update(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        CategoryUploadVM categoryUpload = new()
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            CategoryImage = category.CategoryImageUrl,
        };
        return View(categoryUpload);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, CategoryUploadVM category)
    {
        if (!ModelState.IsValid) return View(category);
        Category? category1 = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (category1 == null) return NotFound();
        if (category.Image != null)
        {
            try
            {
                string filename = await _fileservice.UploadFile(category.Image, _web.WebRootPath, 300, "assets", "photos", "Category");
                _fileservice.RemoveFile(_web.WebRootPath, category1.CategoryImageUrl);
                Category category2 = new()
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    CategoryImageUrl = category.CategoryImage
                };
                category1 = category2;
                category1.CategoryImageUrl = filename;
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(category);
            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(category);
            }
        }
        else
        {
            category.CategoryImage = category1.CategoryImageUrl;
            Category category2 = new()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryImageUrl = category.CategoryImage
            };
            category1 = category2;
        }
        //return Content(_context.Entry(sliderdb).State.ToString());
        _context.Categories.Update(category1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
