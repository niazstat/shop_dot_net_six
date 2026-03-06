using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddEmployeesCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Company_UserId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoadNo",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HouseNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RoadNo",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Company_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
