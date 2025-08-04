// Repositories/CategoryRepository.cs
using MyRazorProject.Models;
using MyRazorProject.Repository.IRespository;
using Thecoreappnow.Data;

namespace MyRazorProject.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();

        public Category? GetById(int id) => _context.Categories.FirstOrDefault(c => c.Id == id);

        public void Add(Category category) => _context.Categories.Add(category);

        public void Update(Category category) => _context.Categories.Update(category);

        public void Delete(Category category) => _context.Categories.Remove(category);

        public void Save() => _context.SaveChanges();
    }
}
