// Repositories/ProductRepository.cs
using Microsoft.EntityFrameworkCore;
using MyRazorProject.Models;
using MyRazorProject.Repository.IRespository;
using Thecoreappnow.Data;

namespace MyRazorProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllWithCategory()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public Product GetFirstOrDefault(Func<Product, bool> predicate)
        {
            return _context.Products.FirstOrDefault(predicate);
        }


        public IEnumerable<Product> GetAll() => _context.Products.ToList();

        public Product? GetById(int id) => _context.Products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product) => _context.Products.Add(product);

        public void Update(Product product) => _context.Products.Update(product);

        public void Delete(Product product) => _context.Products.Remove(product);

        public void Save() => _context.SaveChanges();
    }
}
