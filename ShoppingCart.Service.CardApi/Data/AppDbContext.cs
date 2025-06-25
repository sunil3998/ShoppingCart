using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.CartApi.Data;
using ShoppingCart.Service.CartApi.Models;
using System.Diagnostics;

namespace ShoppingCart.Service.CartApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<CartHeaderModel> CartHeaderModels { set; get; }
        public DbSet<CartDetailsModel> CartDetailsModels { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartHeaderModel>()
                .HasMany(c => c.CartDetailsModels)
                .WithOne(cd => cd.CartHeaders)
                .HasForeignKey(cd => cd.CartHeaderId);
        }
    }
}
