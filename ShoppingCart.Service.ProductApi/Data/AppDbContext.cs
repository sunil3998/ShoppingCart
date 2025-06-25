using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.ProductApi.Models;

namespace ShoppingCart.Service.ProductApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<ProductModel> Products { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 1,
                Name = "Product test",
                Description = "Product test Description",
                Price = 200,
                CategoryName = "Electronics",
                ImageUrl ="",
                IsActive = true
            });
        }
    }
}
