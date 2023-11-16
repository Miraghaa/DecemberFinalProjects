using December.Business.Services.Interfaces;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public DashboardController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Profile()
    {
        return View();
    }
    public async Task<IActionResult> Contacts()
    {
         List<Contact> Contacts = await _context.Contacts.ToListAsync();
         return View(Contacts);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Contact? contact = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null) return RedirectToAction("Error", "Dashboard");
        return View(contact);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Contact? contact = await _context.Contacts.FindAsync(id);
        if (contact == null) return NotFound();

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Contacts));
    }
    public IActionResult Error()
    {
        return View();
    }
}
