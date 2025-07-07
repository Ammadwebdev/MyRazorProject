using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRazorProject.Models;
using System.Linq;
using Thecoreappnow.Data;

namespace MyRazorProject.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList(); // Assuming _db is your DbContext
            return View(categoryList);
        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }


            Category? categoryFromDb = _db.Categories.Find(id);

        
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", " The name explicitly cannot match the name");
            }


            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", " Test is an invalid value");
            }



            if ((ModelState.IsValid))
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View();

        }

        // GET: /Category/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }


        // POST: /Category/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == obj.Id);
            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }




        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", " The name explicitly cannot match the name");
            }


            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", " Test is an invalid value");
            }



            if ((ModelState.IsValid))
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View();

        }


    }


    }