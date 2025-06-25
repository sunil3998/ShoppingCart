using Microsoft.EntityFrameworkCore;
using ShoppingCart.Coupon.Models;

namespace ShoppingCart.Coupon.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<CouponModel> Coupons { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CouponModel>().HasData(new CouponModel
            {
                Id = 1,
                Title = "Test",
                Type = "Percentage",
                Discount = 200,
                MinimumAmount = 50,
                CouponPicture = new byte[0],
                IsActive = true
            });
        }
    }
}
