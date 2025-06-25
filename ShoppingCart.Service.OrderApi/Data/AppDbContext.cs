using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.OrderApi.Models;
using System.Diagnostics;

namespace ShoppingCart.Service.OrderApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<OrderHeaderModel> OrderHeaders { set; get; }
        public DbSet<OrderDetailsModel> OrderDetails { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderHeaderModel>()
                .HasMany(c => c.OrderDetails)
                .WithOne(cd => cd.OrderHeader)
                .HasForeignKey(cd => cd.OrdertHeaderId);
        }
    }
}
