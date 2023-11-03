using December.Core.Entities;
using December.Core.Enums;
using FinalProjectsDecmeberUI.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectsDecmeberUI.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager,
                          SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM user)
    {
        if(!ModelState.IsValid) return View(user);
        AppUser newUser = new()
        {
            FirstName = user.FirstName,
            UserName = user.Username,
            Email = user.Email,
        };
        IdentityResult role =await _userManager.CreateAsync(newUser, user.Password);
        if(!role.Succeeded)
        {
            foreach(var error  in role.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(user);
        }
        await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());
        return RedirectToAction(nameof(Login));
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login)
    {
        if (!ModelState.IsValid) return View(login);
        AppUser userdb = await _userManager.FindByEmailAsync(login.Email);
        if (userdb == null)
        {
            ModelState.AddModelError("", "Email or Password is wrong");
            return View(login);
        }

        var signInResult =
            await _signInManager.PasswordSignInAsync(userdb, login.Password, login.RememberMe, true);
        if (signInResult.IsLockedOut)
        {
            ModelState.AddModelError("", "Try few minutes later");
            return View(login);
        }
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Email or Password is wrong");
            return View(login);
        }
        return RedirectToAction(nameof(Index), "Home");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Index), "Home");
    }

}
