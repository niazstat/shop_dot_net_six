using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCustomerBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Suppliers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Customer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Customer");
        }
    }
}
