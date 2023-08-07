using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropsShopProject.Migrations
{
    /// <inheritdoc />
    public partial class addFavoritesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "carts",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    buyerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorites", x => new { x.buyerId, x.productId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "carts",
                newName: "id");
        }
    }
}
