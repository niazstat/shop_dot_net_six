using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSupplierCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerSubCategoryID",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierCategoryID",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SupplierCategorys",
                columns: table => new
                {
                    SupplierCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierCategoryName = table.Column<string>(nullable: true),
                    SupplierCategoryID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierCategorys", x => x.SupplierCategoryID);
                    table.ForeignKey(
                        name: "FK_SupplierCategorys_SupplierCategorys_SupplierCategoryID1",
                        column: x => x.SupplierCategoryID1,
                        principalTable: "SupplierCategorys",
                        principalColumn: "SupplierCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierSubCategorys",
                columns: table => new
                {
                    SupplierSubCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierSubCategoryName = table.Column<string>(nullable: true),
                    SupplierCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierSubCategorys", x => x.SupplierSubCategoryID);
                    table.ForeignKey(
                        name: "FK_SupplierSubCategorys_SupplierCategorys_SupplierCategoryID",
                        column: x => x.SupplierCategoryID,
                        principalTable: "SupplierCategorys",
                        principalColumn: "SupplierCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CustomerSubCategoryID",
                table: "Suppliers",
                column: "CustomerSubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCategorys_SupplierCategoryID1",
                table: "SupplierCategorys",
                column: "SupplierCategoryID1");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierSubCategorys_SupplierCategoryID",
                table: "SupplierSubCategorys",
                column: "SupplierCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Suppliers",
                column: "CustomerSubCategoryID",
                principalTable: "CustomerSubCategorys",
                principalColumn: "CustomerSubCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "SupplierSubCategorys");

            migrationBuilder.DropTable(
                name: "SupplierCategorys");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CustomerSubCategoryID",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CustomerSubCategoryID",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SupplierCategoryID",
                table: "Suppliers");
        }
    }
}
