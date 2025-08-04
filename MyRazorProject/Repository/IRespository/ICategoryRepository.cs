// Repositories/ICategoryRepository.cs

// Repositories/ICategoryRepository.cs
using MyRazorProject.Models;

namespace MyRazorProject.Repository.IRespository
{

    public interface ICategoryRepository
    {

        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        void Save();
    }
}
