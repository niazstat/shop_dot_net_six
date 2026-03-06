using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddEmployeeAndExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesignationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesignationID);
                    table.ForeignKey(
                        name: "FK_Designations_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesHeads",
                columns: table => new
                {
                    ExpensesHeadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesHeads", x => x.ExpensesHeadID);
                    table.ForeignKey(
                        name: "FK_ExpensesHeads_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesHeads_Company_UserId",
                        column: x => x.UserId,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeCode = table.Column<int>(nullable: false),
                    GeneratedCode = table.Column<string>(nullable: true, computedColumnSql: "CONVERT(VARCHAR, 1000 +  EmployeeID )"),
                    Name = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    CurrentAddress = table.Column<string>(nullable: true),
                    DesignationID = table.Column<int>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Mobile1 = table.Column<string>(nullable: true),
                    Mobile2 = table.Column<string>(nullable: true),
                    Salary = table.Column<decimal>(nullable: false),
                    Religion = table.Column<string>(nullable: true),
                    BloodGroup = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    Thana = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Division = table.Column<string>(nullable: true),
                    MaleFemale = table.Column<string>(nullable: true),
                    IsLedgerClose = table.Column<bool>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationID",
                        column: x => x.DesignationID,
                        principalTable: "Designations",
                        principalColumn: "DesignationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Company_UserId",
                        column: x => x.UserId,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TranDate = table.Column<DateTime>(nullable: false),
                    AutoSLNo = table.Column<int>(nullable: false),
                    GeneratedAutoSLNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,TranDate))+'-'+CONVERT( VARCHAR,AutoSLNo)"),
                    ExpensesHeadID = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseID);
                    table.ForeignKey(
                        name: "FK_Expenses_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpensesHeads_ExpensesHeadID",
                        column: x => x.ExpensesHeadID,
                        principalTable: "ExpensesHeads",
                        principalColumn: "ExpensesHeadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Company_UserId",
                        column: x => x.UserId,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designations_CompanyID",
                table: "Designations",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyID",
                table: "Employees",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationID",
                table: "Employees",
                column: "DesignationID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CompanyID",
                table: "Expenses",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpensesHeadID",
                table: "Expenses",
                column: "ExpensesHeadID");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesHeads_CompanyID",
                table: "ExpensesHeads",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesHeads_UserId",
                table: "ExpensesHeads",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "ExpensesHeads");
        }
    }
}
