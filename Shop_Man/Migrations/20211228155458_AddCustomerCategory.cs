using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCustomerCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerSubCategoryID",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerSubCategoryName",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disrict",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningCommission",
                table: "Customer",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningQty",
                table: "Customer",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CustomerCategorys",
                columns: table => new
                {
                    CustomerCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCategorys", x => x.CustomerCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSubCategorys",
                columns: table => new
                {
                    CustomerSubCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerSubCategoryName = table.Column<string>(nullable: true),
                    CustomerCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSubCategorys", x => x.CustomerSubCategoryID);
                    table.ForeignKey(
                        name: "FK_CustomerSubCategorys_CustomerCategorys_CustomerCategoryID",
                        column: x => x.CustomerCategoryID,
                        principalTable: "CustomerCategorys",
                        principalColumn: "CustomerCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerSubCategoryID",
                table: "Customer",
                column: "CustomerSubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubCategorys_CustomerCategoryID",
                table: "CustomerSubCategorys",
                column: "CustomerCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Customer",
                column: "CustomerSubCategoryID",
                principalTable: "CustomerSubCategorys",
                principalColumn: "CustomerSubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerSubCategorys");

            migrationBuilder.DropTable(
                name: "CustomerCategorys");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerSubCategoryID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerSubCategoryID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerSubCategoryName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Disrict",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "OpeningCommission",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "OpeningQty",
                table: "Customer");
        }
    }
}
