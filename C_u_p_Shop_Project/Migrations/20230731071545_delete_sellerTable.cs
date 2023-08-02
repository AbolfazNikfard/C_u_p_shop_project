using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropsShopProject.Migrations
{
    /// <inheritdoc />
    public partial class deletesellerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_sellers_sellerId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_products_sellers_sellerId",
                table: "products");

            migrationBuilder.DropTable(
                name: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_products_sellerId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_orders_sellerId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "StockMassUnit",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "products");

            migrationBuilder.DropColumn(
                name: "WeightMassUnit",
                table: "products");

            migrationBuilder.DropColumn(
                name: "confirmation",
                table: "products");

            migrationBuilder.DropColumn(
                name: "sellerId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "WeightMassUnit",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "sellerId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "products",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Stock",
                table: "products",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StockMassUnit",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightMassUnit",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "confirmation",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sellerId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightMassUnit",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sellerId",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sellers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellers", x => x.id);
                    table.ForeignKey(
                        name: "FK_sellers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_sellerId",
                table: "products",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_sellerId",
                table: "orders",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_userId",
                table: "sellers",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_sellers_sellerId",
                table: "orders",
                column: "sellerId",
                principalTable: "sellers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_sellers_sellerId",
                table: "products",
                column: "sellerId",
                principalTable: "sellers",
                principalColumn: "id");
        }
    }
}
