using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class tablesNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.CreateTable(
                name: "Table1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field10 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field10 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table2", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table1");

            migrationBuilder.DropTable(
                name: "Table2");

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                });
        }
    }
}
