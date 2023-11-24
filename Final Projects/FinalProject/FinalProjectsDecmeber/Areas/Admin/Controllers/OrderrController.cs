using December.Core.Entities;
using December.Core.Entities.Areas;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class OrderrController : Controller
{
    private readonly AppDbContext _context;

    public OrderrController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int id)
    {
        List<Basket> baskets = await _context.Baskets.ToListAsync();
        return View(baskets);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Basket? basket = await _context.Baskets.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (basket == null) return RedirectToAction("Error", "Dashboard");
        return View(basket);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Basket basket = await _context.Baskets.FindAsync(id);
        if (basket == null) return RedirectToAction("Error", "Dashboard");
        Order orders = await _context.Orders.FindAsync(id);
        _context.Baskets.Remove(basket);    
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}
