using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddEditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEntryEdits",
                columns: table => new
                {
                    LogEntryEditID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EditItem = table.Column<string>(nullable: true),
                    EditType = table.Column<string>(nullable: true),
                    ChallanID = table.Column<int>(nullable: false),
                    ChallanDetailsID = table.Column<int>(nullable: false),
                    dDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Remarks = table.Column<string>(nullable: true),
                    IsProcessDone = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntryEdits", x => x.LogEntryEditID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntryEdits");
        }
    }
}
