using December.Business.ViewModels.AreasViewModels.SizeVMs;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class SizeController : Controller
{
    private readonly AppDbContext _context;
    public SizeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index(int id)
    {
        Size size = _context.Sizes.FirstOrDefault(s => s.Id == id);
        List<Size> sizes = _context.Sizes.ToList();
        return View(sizes);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SizeListVM size)
    {
        if (!ModelState.IsValid) return View(size);
        Size size1 = new()
        {
            Type = size.SizeName
        };
        await _context.Sizes.AddAsync(size1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Size? size = await _context.Sizes.FindAsync(id);
        if (size == null) return NotFound();
        return View(size);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Size? size = await _context.Sizes.FindAsync(id);
        if (size == null) return NotFound();
        _context.Sizes.Remove(size);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}
