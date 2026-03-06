using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddDayCloseDetails2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayCloseDetailses",
                columns: table => new
                {
                    DayCloseDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dDate = table.Column<DateTime>(nullable: false),
                    ChallanNoID = table.Column<int>(nullable: false),
                    ChallanNo = table.Column<string>(nullable: true),
                    TransType = table.Column<string>(nullable: true),
                    Describtion = table.Column<string>(nullable: true),
                    ReceiveAmount = table.Column<decimal>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCloseDetailses", x => x.DayCloseDetailsID);
                    table.ForeignKey(
                        name: "FK_DayCloseDetailses_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayCloseDetailses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayCloseDetailses_CompanyID",
                table: "DayCloseDetailses",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DayCloseDetailses_UserId",
                table: "DayCloseDetailses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayCloseDetailses");
        }
    }
}
