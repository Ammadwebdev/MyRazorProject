using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRazorProject.Models;
using MyRazorProject.Repository.IRespository;

namespace MyRazorProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryRepo.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            return View(new Product());
        }


        public IActionResult Index()
        {
            var allProducts = _productRepo.GetAllWithCategory();
            return View(allProducts);
        }


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




        public IActionResult Edit(int? id)
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
