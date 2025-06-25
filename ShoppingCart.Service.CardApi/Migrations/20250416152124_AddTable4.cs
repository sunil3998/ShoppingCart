using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Service.CartApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderModelId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CartHeaderModelId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "CartHeaderModelId",
                table: "CartDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.AddColumn<int>(
                name: "CartHeaderModelId",
                table: "CartDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartHeaderModelId",
                table: "CartDetails",
                column: "CartHeaderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderModelId",
                table: "CartDetails",
                column: "CartHeaderModelId",
                principalTable: "CartHeaders",
                principalColumn: "Id");
        }
    }
}
