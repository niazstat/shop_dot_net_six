using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSupplierCategoryCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "CustomerSubCategoryID",
                table: "Suppliers",
                newName: "SupplierSubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_CustomerSubCategoryID",
                table: "Suppliers",
                newName: "IX_Suppliers_SupplierSubCategoryID");

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

            migrationBuilder.RenameColumn(
                name: "SupplierSubCategoryID",
                table: "Suppliers",
                newName: "CustomerSubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_SupplierSubCategoryID",
                table: "Suppliers",
                newName: "IX_Suppliers_CustomerSubCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Suppliers",
                column: "CustomerSubCategoryID",
                principalTable: "CustomerSubCategorys",
                principalColumn: "CustomerSubCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
