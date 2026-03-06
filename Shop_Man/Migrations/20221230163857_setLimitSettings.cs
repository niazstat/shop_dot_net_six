using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class setLimitSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "SalesReturns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "SalesReturns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "SalesReturns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "SalesHeads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "SalesHeads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "SalarySheets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "SalarySheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "PurchaseHeads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "PurchaseHeads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "PurchaseHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "PurchaseDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "PurchaseDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "PurchaseDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "ExpensesHeads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumLimit",
                table: "ExpensesHeads",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "ExpensesHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Expenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxSalaryLimit",
                table: "Employees",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Designations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Designations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Designations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "DayCloses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "DayCloses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "DayCloses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "CustomerSubCategorys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "CustomerSubCategorys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "CustomerSubCategorys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "CustomerCategorys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "CustomerCategorys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "CustomerCategorys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MaxBalanceLimit",
                table: "Customer",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "CompanyProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "CompanyProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "CompanyProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Company",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Company",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "ChequeTransactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "ChequeTransactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "ChequeTransactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "CashReceive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "CashReceive",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "CashReceive",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "CashPayments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "CashPayments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "CashPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Banks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Banks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Banks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "BankAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "BankAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowEdit",
                table: "Adjustments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxAdjLimit",
                table: "Adjustments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserID",
                table: "Adjustments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LimitSettings",
                columns: table => new
                {
                    LimitSettingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dDate = table.Column<DateTime>(nullable: false),
                    Particular = table.Column<string>(nullable: true),
                    LimitAmount = table.Column<decimal>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    ExpensesHeadID = table.Column<int>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitSettings", x => x.LimitSettingID);
                    table.ForeignKey(
                        name: "FK_LimitSettings_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LimitSettings_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LimitSettings_ExpensesHeads_ExpensesHeadID",
                        column: x => x.ExpensesHeadID,
                        principalTable: "ExpensesHeads",
                        principalColumn: "ExpensesHeadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LimitSettings_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LimitSettings_CustomerID",
                table: "LimitSettings",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_LimitSettings_EmployeeID",
                table: "LimitSettings",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LimitSettings_ExpensesHeadID",
                table: "LimitSettings",
                column: "ExpensesHeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LimitSettings_SupplierId",
                table: "LimitSettings",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LimitSettings");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "SalesDetailss");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "SalesDetailss");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "SalesDetailss");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "SalarySheets");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "SalarySheets");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "PurchaseHeads");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "PurchaseHeads");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "PurchaseHeads");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "ExpensesHeads");

            migrationBuilder.DropColumn(
                name: "MaximumLimit",
                table: "ExpensesHeads");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "ExpensesHeads");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MaxSalaryLimit",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "CustomerSubCategorys");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "CustomerSubCategorys");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "CustomerSubCategorys");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "CustomerCategorys");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "CustomerCategorys");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "CustomerCategorys");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "MaxBalanceLimit",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "CompanyProducts");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "CompanyProducts");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "CompanyProducts");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "CashReceive");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "CashReceive");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "CashReceive");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "IsAllowEdit",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "MaxAdjLimit",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "UpdateUserID",
                table: "Adjustments");
        }
    }
}
