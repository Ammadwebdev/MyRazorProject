using Microsoft.EntityFrameworkCore;
using MyRazorProject.Models;

namespace Thecoreappnow.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
    new Category { Id = 1, Name = "Knee Sleeves", DisplayOrder = 1 },
    new Category { Id = 2, Name = "Lifting Straps", DisplayOrder = 2 },
    new Category { Id = 3, Name = "T-Shirts", DisplayOrder = 3 }
          );





        }

    }
}
