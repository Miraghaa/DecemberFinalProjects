using December.Core.Entities;
using December.Core.Enums;
using FinalProjectsDecmeberUI.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace FinalProjectsDecmeberUI.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration Configuration;

    public AuthController(UserManager<AppUser> userManager,
                          SignInManager<AppUser> signInManager,
                          IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        Configuration = configuration;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        var newUser = new AppUser
        {
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            UserName = user.Username,
            Email = user.Email,
        };

        var registrationResult = await _userManager.CreateAsync(newUser, user.Password);

        if (!registrationResult.Succeeded)
        {
            foreach (var error in registrationResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }

        await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

        var smtpClient = new SmtpClient("smtp.mail.ru")
        {
            Port = 587,
            Credentials = new NetworkCredential(Configuration["AccountConfirm:Mail"], Configuration["AccountConfirm:Password"]),
            EnableSsl = true,
        };
        var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = newUser.Id, token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser) }, Request.Scheme);
        var mailMessage = new MailMessage
        {
            From = new MailAddress(Configuration["AccountConfirm:Mail"], "Account Confirmation"),
            Subject = "Account Confirmation",
            Body = $"Your account has been created successfully. Please click this link to confirm your account: <a href=\"{confirmationLink}\">Approve</a>",
            IsBodyHtml = true,
        };

        mailMessage.To.Add(user.Email);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while sending the email.");
            return View(user);
        }

        return RedirectToAction("Index","Home");
    }
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return View("Register");
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Login));
        }
        else
        {
            return View("Register");
        }
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
        if (!userdb.EmailConfirmed)
        {
            ModelState.AddModelError("", "You cannot log in without confirming your e-mail.");
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
