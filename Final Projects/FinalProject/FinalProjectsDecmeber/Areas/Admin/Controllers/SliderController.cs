using AutoMapper;
using December.Business.Exceptions;
using December.Business.Services.Interfaces;
using December.Business.ViewModels.AreasViewModels.SliderVMs;
using December.Core.Entities.Areas;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _web;
    private readonly IFileService _fileservice;

    public SliderController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice,
                            IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        _web = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index()
    {
        var slider = _context.Sliders.AsNoTracking();
        if (slider == null) return NotFound();
        ViewBag.Count = slider.Count();
        return View(slider);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return RedirectToAction("Error","Dashboard");
        return View(slider);
    }
    public IActionResult Create()
    {
        if (_context.Sliders.Count() >= 5)
        {
            return BadRequest();
        }
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SliderCreateVM slidercreate)
    {
        if(!ModelState.IsValid) return View(slidercreate);
        string filename = string.Empty;
        try
        {
            filename = await _fileservice.UploadFile(slidercreate.SliderIamgeUrl, _web.WebRootPath, 1000, "assets", "photos", "slider");
            Slider slider = _mapper.Map<Slider>(slidercreate);
            slider.SliderIamgeUrl = filename;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }
        catch(FileSizeException ex)
        {
            ModelState.AddModelError("SliderIamgeUrl", ex.Message);
            return View(slidercreate);
        }
        catch (FileTypeException ex)
        {
            ModelState.AddModelError("SliderIamgeUrl",ex.Message);
            return View(slidercreate);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(slidercreate);
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return RedirectToAction("Error", "Dashboard");
        string fileroot = Path.Combine(_web.WebRootPath, slider.SliderIamgeUrl);
        if (System.IO.File.Exists(fileroot))
        {
            System.IO.File.Delete(fileroot);
        }
        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return RedirectToAction("Error", "Dashboard");
        SliderUpdateVM sliderUpdate = _mapper.Map<SliderUpdateVM>(slider);
        return View(sliderUpdate);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, SliderUpdateVM slider)
    {
        if (!ModelState.IsValid) return View(slider);
        Slider? sliderdb = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (sliderdb == null) return NotFound();
        if (slider.Image != null)
        {
            try
            {
                string filename = await _fileservice.UploadFile(slider.Image, _web.WebRootPath, 300, "assets", "photos", "slider");
                _fileservice.RemoveFile(_web.WebRootPath, sliderdb.SliderIamgeUrl);
                sliderdb = _mapper.Map<Slider>(slider);
                sliderdb.SliderIamgeUrl = filename;
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(slider);
            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(slider);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(slider);
            }
        }
        else
        {
            slider.SliderIamgeUrl = sliderdb.SliderIamgeUrl;
            sliderdb = _mapper.Map<Slider>(slider);
        }
        _context.Sliders.Update(sliderdb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
