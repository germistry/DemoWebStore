using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Database.Migrations
{
    public partial class ChangeFieldNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "StockName",
                table: "Stock",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinValue",
                table: "Products",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockName",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
