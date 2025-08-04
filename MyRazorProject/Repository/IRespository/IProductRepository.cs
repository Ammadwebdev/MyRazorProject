// Repositories/IRespository/IProductRepository.cs
using MyRazorProject.Models;

namespace MyRazorProject.Repository.IRespository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllWithCategory();

        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Save();
    }
}
