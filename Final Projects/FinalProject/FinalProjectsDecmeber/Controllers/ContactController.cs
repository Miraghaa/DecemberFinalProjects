using AutoMapper;
using December.Business.Services.Interfaces;
using December.Business.ViewModels.ContactVMs;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectsDecmeberUI.Controllers;

public class ContactController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    private readonly IMapper _mapper;
    public ContactController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IMapper mapper,
                            IFileService fileservice)
    {
        _mapper = mapper;
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ContactCreateVM contact)
    {
        if (!ModelState.IsValid) return View(contact);
        try
        {
            Contact contacts = _mapper.Map<Contact>(contact);
            _context.Contacts.Add(contacts);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(contact);
        }
        return RedirectToAction(nameof(Index));
    }
}
