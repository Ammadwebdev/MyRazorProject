using Microsoft.EntityFrameworkCore;
using MyRazorProject.Models;

namespace Thecoreappnow.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Knee Sleeves", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Lifting Straps", DisplayOrder = 2 },
                new Category { Id = 3, Name = "T-Shirts", DisplayOrder = 3 }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "SBD Knee Sleeves",
                    Author = "SBD Qatar",
                    Description = "High-quality knee sleeves designed to provide support and stability during heavy lifts.",
                    ISBN = "SBD-KS-001",
                    ListPrice = 2500,
                    Price = 442,
                    Price50 = 1000,
                    Price100 = 399,
                    CategoryId = 1,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Title = "SBD Lifting Straps",
                    Author = "SBD Qatar",
                    Description = "Durable lifting straps to improve grip and reduce strain during pulling exercises.",
                    ISBN = "SBD-LS-002",
                    ListPrice = 120.00,
                    Price = 110.00,
                    Price50 = 100.00,
                    Price100 = 90.00,
                    CategoryId = 2,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 3,
                    Title = "SBD Training T-Shirt - Black",
                    Author = "SBD Qatar",
                    Description = "Comfortable and breathable T-shirt designed for performance during training.",
                    ISBN = "SBD-TS-003",
                    ListPrice = 95.00,
                    Price = 85.00,
                    Price50 = 80.00,
                    Price100 = 75.00,
                    CategoryId = 3,
                    ImageUrl = ""

                },
                new Product
                {
                    Id = 4,
                    Title = "SBD T-Shirt - Red Limited Edition",
                    Author = "SBD Qatar",
                    Description = "Limited edition SBD T-shirt in red with premium cotton material.",
                    ISBN = "SBD-TS-004",
                    ListPrice = 110.00,
                    Price = 99.00,
                    Price50 = 94.00,
                    Price100 = 89.00,
                    CategoryId = 3, // ✅ Fixed: Using existing category ID
                    ImageUrl = ""

                }
            );
        }
    }
}
