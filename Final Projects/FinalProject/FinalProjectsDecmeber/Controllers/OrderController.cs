using December.Business.Contacts.Email;
using December.Business.Services.Interfaces;
using December.Business.ViewModels.BasketVMs;
using December.Business.ViewModels.OrderVMs;
using December.Core.Entities;
using December.Core.Entities.Areas;
using December.DataAccess.contexts;
using FinalProjectsDecmeberUI.ViewModels.ContactVMs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FinalProjectsDecmeberUI.Controllers;
public class OrderController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    public IEmailService _emailService { get; set; }


    public OrderController(UserManager<AppUser> userManager, 
                           AppDbContext context, 
                           IEmailService emailService)
    {
        _userManager = userManager;
        _context = context;
        _emailService = emailService;
    }
    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userName = User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var addresses = await _context.Adresses
                        .AsNoTracking()
                        .Where(a => a.UserName == user.UserName)
                        .ToListAsync();

                    var contactVM = new ContactVM
                    {
                        AppUser = user,
                        Adresses = addresses,
                    };

                    return View(contactVM);
                }
            }
        }
        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public JsonResult Basket([FromBody] List<BasketCreateVM> basketData)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = "Model geçersiz. Hatalar: " + string.Join(", ", errors) });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Kullanıcı girişi yapılmamış." });
            }

            if (basketData != null && basketData.Any())
            {
                foreach (var item in basketData)
                {
                  
                    var basketEntity = new Basket
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Count = item.Count,
                        IamgeUrl = item.IamgeUrl,
                        UserIds = userId
                    };

                    _context.Baskets.Add(basketEntity);
                }
                _context.SaveChanges();

                return Json(new { success = true, message = "Sepet verileri başarıyla veritabanına eklendi." });
            }
            else
            {
                return Json(new { success = false, message = "Sepet verileri boş veya geçersiz." });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Hata oluştu: " + ex.Message });
        }
    }
    [HttpPost]
    public async Task<IActionResult> Create(OrderVM order)
    {
        try
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser is null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order orders = new()
            {
                City = order.City,
                Country = order.Country,
                State = order.State,
                PhoneNumber = order.PhoneNumber,
                Postcode = order.Postcode,
                Email = order.Email,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Address = order.Address,
                UserIds = userId
            };

            _context.Orders.Add(orders);
            _context.SaveChanges();

            var stausMessageDto = PrepareStausMessage(currentUser.Email);

            _emailService.Send(stausMessageDto);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Hata oluştu: " + ex.Message });
        }
    }

    MessageVM PrepareStausMessage(string email)
    {
        string body = "ReserVation Has Been Successfully Finished";
        string subject = EmailMessages.Subject.NOTIFICATION_MESSAGE;
        return new MessageVM(email, subject, body);
    }


}


