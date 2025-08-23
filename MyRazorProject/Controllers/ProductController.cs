using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRazorProject.Models;
using MyRazorProject.Repository.IRespository;
using MyRazorProject.ViewModels;
using System.Linq;

namespace MyRazorProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(
            IProductRepository productRepo,
            ICategoryRepository categoryRepo,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var allProducts = _productRepo.GetAllWithCategory();
            return View(allProducts);
        }




        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var product = id == null ? new Product() : _productRepo.GetFirstOrDefault(p => p.Id == id.Value);

            if (product == null)
                return NotFound();

            var productVM = new ProductVM
            {
                Product = product,
                CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(productVM); // This will render Upsert.cshtml
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                // ✅ Set a default image URL if none provided
                if (string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    productVM.Product.ImageUrl = "/images/products/default.png"; // <-- Default path
                }

                _productRepo.Add(productVM.Product);
                _productRepo.Save();

                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            // If model state is invalid, repopulate the CategoryList
            productVM.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(productVM);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(ProductVM productVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _productRepo.Add(productVM.Product);
        //        _productRepo.Save();

        //        TempData["success"] = "Product created successfully!";
        //        return RedirectToAction("Index");
        //    }

        //    // If model state is invalid, repopulate the CategoryList
        //    productVM.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString()
        //    });

        //    return View(productVM);
        //}


        //public IActionResult Index()
        //{
        //    var allProducts = _productRepo.GetAllWithCategory();

        //    return View(allProducts);
        //    // Create a SelectList from products

        //}






        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if (obj.Title?.ToLower() == "test")
                ModelState.AddModelError("", "Test is an invalid value");

            if (ModelState.IsValid)
            {
                _productRepo.Add(obj);
                _productRepo.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            // Repopulate ViewBag.CategoryList so the dropdown can render again
            ViewBag.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Product obj, IFormFile? imageFile)
        //{
        //    // Example of a custom validation rule
        //    if (obj.Title?.ToLower() == "test")
        //        ModelState.AddModelError("", "Test is an invalid value");

        //    // Validate file if required
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        ModelState.AddModelError("ImageUrl", "Please upload an image.");
        //    }
        //    else
        //    {
        //        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        //        var extension = Path.GetExtension(imageFile.FileName).ToLower();

        //        if (!allowedExtensions.Contains(extension))
        //        {
        //            ModelState.AddModelError("ImageUrl", "Only JPG, PNG, or GIF files are allowed.");
        //        }

        //        if (imageFile.Length > 2 * 1024 * 1024) // 2MB max
        //        {
        //            ModelState.AddModelError("ImageUrl", "File size cannot exceed 2MB.");
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //        var uploadPath = Path.Combine(wwwRootPath, "images", "products");

        //        if (!Directory.Exists(uploadPath))
        //            Directory.CreateDirectory(uploadPath);

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        //        var filePath = Path.Combine(uploadPath, fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            imageFile.CopyTo(stream);
        //        }

        //        obj.ImageUrl = "/images/products/" + fileName;

        //        _productRepo.Add(obj);
        //        _productRepo.Save();
        //        TempData["success"] = "Product created successfully!";
        //        return RedirectToAction("Index");
        //    }

        //    // If validation fails, re-populate dropdown
        //    ViewBag.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString()
        //    });

        //    return View(obj);
        //}




        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var product = _productRepo.GetById(id.Value);
            if (product == null)
                return NotFound();

            // Set ViewBag.CategoryList
            ViewBag.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            if (obj.Title?.ToLower() == "test")
                ModelState.AddModelError("", "Test is an invalid value");

            if (ModelState.IsValid)
            {
                _productRepo.Update(obj);
                _productRepo.Save();
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }

            // Set ViewBag.CategoryList again for dropdown to work on validation failure
            ViewBag.CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var product = _productRepo.GetById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearTitle(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null)
                return NotFound();

            product.Title = string.Empty;
            product.Description = string.Empty;
            _productRepo.Update(product);
            _productRepo.Save();

            TempData["success"] = "Product title cleared!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null)
                return NotFound();

            _productRepo.Delete(product);
            _productRepo.Save();
            TempData["success"] = "Product deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
