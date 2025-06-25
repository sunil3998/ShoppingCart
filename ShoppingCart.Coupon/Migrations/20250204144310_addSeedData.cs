using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Coupon.Migrations
{
    /// <inheritdoc />
    public partial class addSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "CouponPicture", "Discount", "IsActive", "MinimumAmount", "Title", "Type" },
                values: new object[] { 1, new byte[0], 200.0, true, 50.0, "Test", "Percentage" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
