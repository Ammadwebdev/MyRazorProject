using MyRazorProject.Repositories;
using Thecoreappnow.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRazorProject.Repository.IRespository
{
    public class UnitOfWork
    {
        private ApplicationDbContext _context;
      public  ICategoryRepository Category { get; private set; }
      public IProductRepository Product { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new  ProductRepository(_context);
        }
        
        
        void Save()
        {

            _context.SaveChanges();
        }
    }
}
