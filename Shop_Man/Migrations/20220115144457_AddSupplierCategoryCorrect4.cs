using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSupplierCategoryCorrect4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierSubCategoryID",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierSubCategoryID",
                table: "Suppliers",
                column: "SupplierSubCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_SupplierSubCategorys_SupplierSubCategoryID",
                table: "Suppliers",
                column: "SupplierSubCategoryID",
                principalTable: "SupplierSubCategorys",
                principalColumn: "SupplierSubCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_SupplierSubCategorys_SupplierSubCategoryID",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_SupplierSubCategoryID",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SupplierSubCategoryID",
                table: "Suppliers");
        }
    }
}
