using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalarySheetHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalarySheetHeadID",
                table: "SalarySheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Expenses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalarySheetHeads",
                columns: table => new
                {
                    SalarySheetHeadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySheetHeads", x => x.SalarySheetHeadID);
                    table.ForeignKey(
                        name: "FK_SalarySheetHeads_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalarySheetHeads_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheets_SalarySheetHeadID",
                table: "SalarySheets",
                column: "SalarySheetHeadID");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmployeeID",
                table: "Expenses",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheetHeads_CompanyID",
                table: "SalarySheetHeads",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheetHeads_UserId",
                table: "SalarySheetHeads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Employees_EmployeeID",
                table: "Expenses",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalarySheets_SalarySheetHeads_SalarySheetHeadID",
                table: "SalarySheets",
                column: "SalarySheetHeadID",
                principalTable: "SalarySheetHeads",
                principalColumn: "SalarySheetHeadID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Employees_EmployeeID",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_SalarySheets_SalarySheetHeads_SalarySheetHeadID",
                table: "SalarySheets");

            migrationBuilder.DropTable(
                name: "SalarySheetHeads");

            migrationBuilder.DropIndex(
                name: "IX_SalarySheets_SalarySheetHeadID",
                table: "SalarySheets");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_EmployeeID",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "SalarySheetHeadID",
                table: "SalarySheets");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Expenses");
        }
    }
}
