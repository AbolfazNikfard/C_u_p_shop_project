using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropsShopProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "sellers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "buyers");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "buyers");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "buyers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "buyers");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "buyers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "sellers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "buyers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "buyers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "buyers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "buyers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "buyers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
