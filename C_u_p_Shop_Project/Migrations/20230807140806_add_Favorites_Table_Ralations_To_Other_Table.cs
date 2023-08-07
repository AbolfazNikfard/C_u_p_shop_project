using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropsShopProject.Migrations
{
    /// <inheritdoc />
    public partial class addFavoritesTableRalationsToOtherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_favorites_productId",
                table: "favorites",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_favorites_buyers_buyerId",
                table: "favorites",
                column: "buyerId",
                principalTable: "buyers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favorites_products_productId",
                table: "favorites",
                column: "productId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favorites_buyers_buyerId",
                table: "favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_favorites_products_productId",
                table: "favorites");

            migrationBuilder.DropIndex(
                name: "IX_favorites_productId",
                table: "favorites");
        }
    }
}
