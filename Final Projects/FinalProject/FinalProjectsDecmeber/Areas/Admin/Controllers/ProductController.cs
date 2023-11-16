using December.Business.Services.Interfaces;
using December.Business.ViewModels.AreasViewModels.ProductVMs;
using December.Core.Entities.Areas;
using December.Core.Entities;
using December.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using December.Business.Utilities.FileConfiguration;
using Microsoft.EntityFrameworkCore;
using December.Business.ViewModels.AreasViewModels.BrandVMs;
using December.Business.Exceptions;
using System.Drawing.Drawing2D;

namespace FinalProjectsDecmeberUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public ProductController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index(int id)
    {
        Product product = _context.Products.FirstOrDefault(p => p.Id == id);
        List<Product> products = _context.Products.Include(p => p.Images).Select(p => new Product()
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Images = p.Images
        }).ToList();
        return View(products);
    }
    public IActionResult Create()
    {
        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Collections = _context.Collections.ToList();
        ViewBag.ProductDetails = _context.productDetails.ToList();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateVM productcreate)
    {

        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Brands = _context.Brands.ToList();

        string filename = string.Empty;
        string filehover = string.Empty;
        string filenames = string.Empty;

        Product newProduct = new()
        {
            ProductName = productcreate.ProductName,
            Price = productcreate.Price,
            Stock = productcreate.Stock,
            Discount = productcreate.Discount,
            CategoryId = productcreate.CategoryId,
            CollectionId = productcreate.CollectionId,
            DetailId = productcreate.DetailId,
            BrandId = productcreate.BrandId,
        };


        if (productcreate.MainImage != null)
        {
            if (!productcreate.MainImage.CheckFileSize(1000))
            {
                return View(nameof(Create));
            };

            if (!productcreate.MainImage.CheckFileType("image/"))
            {
                return View(nameof(Create));
            };

            filename = await _fileservice.UploadFile(productcreate.MainImage, _webEnv.WebRootPath, 10000, "assets", "photos", "Products", "yoxlama");
        }
        if (productcreate.HoverImage != null)
        {
            if (!productcreate.HoverImage.CheckFileSize(1000))
            {
                return View(nameof(Create));
            };

            if (!productcreate.HoverImage.CheckFileType("image/"))
            {
                return View(nameof(Create));
            };

            filehover = await _fileservice.UploadFile(productcreate.HoverImage, _webEnv.WebRootPath, 10000, "assets", "photos", "Products", "yoxlama");
        }
        if (!string.IsNullOrEmpty(filename))
        {
            Image MainImage = new()
            {
                IsMain = true,
                ImageUrl = filename
            };

            newProduct.Images.Add(MainImage);
        }
        if (!string.IsNullOrEmpty(filehover))
        {
            Image Hoverimage = new()
            {
                Hoverimage = true,
                ImageUrl = filehover
            };

            newProduct.Images.Add(Hoverimage);
        }
        foreach (IFormFile image in productcreate.Images)
        {
            if (!image.CheckFileSize(1000))
            {
                return View(nameof(Create));
            };

            if (!image.CheckFileType("image/"))
            {
                return View(nameof(Create));
            };

            filenames = await _fileservice.UploadFile(image, _webEnv.WebRootPath, 10000, "assets", "photos", "Products", "yoxlama");

            Image NotMainImage = new()
            {
                IsMain = false,
                ImageUrl = filenames
            };

            newProduct.Images.Add(NotMainImage);
        }
        foreach (int id in productcreate.ColorIds)
        {
            ProductColor productColor = new()
            {
                ProductId = id,
                ColorId = id,
            };

            newProduct.Colors.Add(productColor);
        }
        foreach (int id in productcreate.SizeIds)
        {
            ProductSize producttSize = new()
            {
                ProductId = id,
                SizeId = id,
            };

            newProduct.Sizes.Add(producttSize);
        }
        if (!productcreate.MainImage.CheckFileSize(1000))
        {
            return View(nameof(Create));
        };

        if (!productcreate.MainImage.CheckFileType("image/"))
        {
            return View(nameof(Create));
        };

        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Product? product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Brands)
            .Include(p => p.Categorys)
            .Include(p => p.Collections)
            .Include(p => p.Sizes).ThenInclude(p => p.Size)
            .Include(p => p.Colors).ThenInclude(p => p.Color)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return RedirectToAction("Error", "Dashboard");
        return View(product);

    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Product? product = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(product => product.Id == id);
        if (product == null) return RedirectToAction("Error", "Dashboard");
        string imageUrl = string.Empty;
        foreach (var image in product.Images)
        {
            imageUrl = image.ImageUrl;
        }
        string fileroot = Path.Combine(_webEnv.WebRootPath, imageUrl);
        if (System.IO.File.Exists(fileroot))
        {
            System.IO.File.Delete(fileroot);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        Product? product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Collections)
            .Include(p => p.Sizes).ThenInclude(p => p.Size)
            .Include(p => p.Colors).ThenInclude(p => p.Color)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return RedirectToAction("Error", "Dashboard");
        ProductListVM productList = new()
        {
            Id = product.Id,
            Price = product.Price,
            Discount = product.Discount,
            Name = product.ProductName,
            Collection = product.Collections,
            Images = product.Images,
            ColorName = product.Colors.Select(color => color.Color.ColorName).ToList(),
            SizeType = product.Sizes.Select(size => size.Size.Type).ToList(),

        };
        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Collections = _context.Collections.ToList();
        ViewBag.ProductDetails = _context.productDetails.ToList();
        return View(productList);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, ProductListVM productList)
    {

        Product? product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Collections)
            .Include(p => p.Sizes).ThenInclude(p => p.Size)
            .Include(p => p.Colors).ThenInclude(p => p.Color)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Collections = _context.Collections.ToList();
        ViewBag.ProductDetails = _context.productDetails.ToList();

        try
        {
            if (product.Images != null)
            {
                string filename = await _fileservice.UploadFile(productList.MainImage, _webEnv.WebRootPath, 10000, "assets", "photos", "Products", "yoxlama");
                string filehover = await _fileservice.UploadFile(productList.HoverImage, _webEnv.WebRootPath, 10000, "assets", "photos", "Products", "yoxlama");

                _fileservice.RemoveFile(_webEnv.WebRootPath, product.Images.FirstOrDefault(img => img.IsMain)?.ImageUrl);
                _fileservice.RemoveFile(_webEnv.WebRootPath, product.Images.FirstOrDefault(img => img.Hoverimage)?.ImageUrl);

                if (productList.Collection != null)
                {
                    Collection collection = await _context.Collections.FindAsync(productList.Collection.Id);
                    if (collection != null)
                    {
                        product.CollectionId = collection.Id;
                        product.Collections = collection;
                    }
                }

                Product updatedProduct = new()
                {
                    Id = productList.Id,
                    ProductName = productList.Name,
                    Stock = productList.Stock,
                    Price = productList.Price,
                    Discount = productList.Discount,
                    Sizes = productList.SizeIds?.Select(sizeId => new ProductSize { SizeId = sizeId }).ToList() ?? new List<ProductSize>(),
                    Colors = productList.ColorIds?.Select(colorid => new ProductColor { ColorId = colorid }).ToList() ?? new List<ProductColor>(),
                };

                updatedProduct.Images = new List<Image>();
                Image mainImage = product.Images.FirstOrDefault(img => img.IsMain);

                if (mainImage != null)
                {
                    mainImage.ImageUrl = filename;
                }

                product.Images.ForEach(img =>
                {
                    updatedProduct.Images.Add(new Image
                    {
                        IsMain = img.IsMain,
                        Hoverimage = img.Hoverimage,
                        ImageUrl = img.ImageUrl
                    });
                });
                product = updatedProduct;
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (FileSizeException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(productList);
        }
        catch (FileTypeException ex)
        {
            ModelState.AddModelError("Image", ex.Message);
            return View(productList);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(productList);
        }
    }
}