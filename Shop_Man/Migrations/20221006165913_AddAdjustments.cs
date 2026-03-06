using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Proprietor",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Company",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Adjustments",
                columns: table => new
                {
                    AdjustmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoicNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,PaymentDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)"),
                    InvoicNoSL = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true),
                    PreviousDue = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustments", x => x.AdjustmentID);
                    table.ForeignKey(
                        name: "FK_Adjustments_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adjustments_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adjustments_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adjustments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_CompanyID",
                table: "Adjustments",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_CustomerID",
                table: "Adjustments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_SupplierId",
                table: "Adjustments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_UserId",
                table: "Adjustments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjustments");

            migrationBuilder.DropColumn(
                name: "Proprietor",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Company");
        }
    }
}
