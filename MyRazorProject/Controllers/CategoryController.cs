using Microsoft.AspNetCore.Mvc;
using MyRazorProject.Models;
using MyRazorProject.Repository.IRespository;

namespace MyRazorProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var categoryList = _categoryRepo.GetAll();
            return View(categoryList);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
                ModelState.AddModelError("name", "The name cannot match the display order");

            if (obj.Name?.ToLower() == "test")
                ModelState.AddModelError("", "Test is an invalid value");

            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _categoryRepo.GetById(id.Value);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
                ModelState.AddModelError("name", "The name cannot match the display order");

            if (obj.Name?.ToLower() == "test")
                ModelState.AddModelError("", "Test is an invalid value");

            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _categoryRepo.GetById(id.Value);
            if (category == null)
                return NotFound();

            return View(category);
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            var category = _categoryRepo.GetById(obj.Id);
            if (category == null)
                return NotFound();

            _categoryRepo.Delete(category);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
