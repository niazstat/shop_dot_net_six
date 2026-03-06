using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalarySheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalarySheets",
                columns: table => new
                {
                    SalarySheetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    YearName = table.Column<int>(nullable: false),
                    MonthName = table.Column<string>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AddAmount = table.Column<decimal>(nullable: false),
                    DeductAmount = table.Column<decimal>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySheets", x => x.SalarySheetID);
                    table.ForeignKey(
                        name: "FK_SalarySheets_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalarySheets_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalarySheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheets_CompanyID",
                table: "SalarySheets",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheets_EmployeeID",
                table: "SalarySheets",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheets_UserId",
                table: "SalarySheets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalarySheets");
        }
    }
}
