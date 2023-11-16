using AutoMapper;
using December.Business.ViewModels.AccountVMs;
using December.Core.Entities;
using December.DataAccess.contexts;
using FinalProjectsDecmeberUI.ViewModels.ContactVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProjectsDecmeberUI.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public AccountController(UserManager<AppUser> userManager, IMapper mapper, AppDbContext context)
    {
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userName = User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var addresses = await _context.Adresses
                        .AsNoTracking()
                        .Where(a => a.UserName == user.UserName)
                        .ToListAsync();
                    var userBaskets = await _context.Baskets
                        .Where(b => b.UserIds == userId)
                        .ToListAsync();

                    var contactVM = new ContactVM
                    {
                        AppUser = user,
                        Adresses = addresses,
                        Baskets = userBaskets
                    };

                    return View(contactVM);
                }
            }
        }
        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdressVM adress)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Error", "Home");

        try
        {
            Adress addressss = _mapper.Map<Adress>(adress);
            _context.Adresses.Add(addressss);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return RedirectToAction("Error", "Home");
        }

        // Adresi oluşturduktan sonra ContactVM içindeki Adress'leri güncelle
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var addresses = await _context.Adresses
            .AsNoTracking()
            .Where(a => a.UserName == user.UserName)
            .ToListAsync();

        var contactVM = new ContactVM
        {
            AppUser = user,
            Adresses = addresses
        };

        return RedirectToAction(nameof(Index), contactVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var addressToDelete = await _context.Adresses.FindAsync(id);

        if (addressToDelete == null) return RedirectToAction("Error", "Home");
        try
        {
            _context.Adresses.Remove(addressToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Silme işlemi sırasında bir hata oluştu.");
            return RedirectToAction(nameof(Index));
        }
    }
}
