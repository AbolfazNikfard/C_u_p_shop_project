using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropsShopProject.Migrations
{
    /// <inheritdoc />
    public partial class RelationBetweenUserAndBuyerAndSeller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "sellers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "buyers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_userId",
                table: "sellers",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_buyers_userId",
                table: "buyers",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_buyers_AspNetUsers_userId",
                table: "buyers",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sellers_AspNetUsers_userId",
                table: "sellers",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_buyers_AspNetUsers_userId",
                table: "buyers");

            migrationBuilder.DropForeignKey(
                name: "FK_sellers_AspNetUsers_userId",
                table: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_sellers_userId",
                table: "sellers");

            migrationBuilder.DropIndex(
                name: "IX_buyers_userId",
                table: "buyers");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "sellers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "buyers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
