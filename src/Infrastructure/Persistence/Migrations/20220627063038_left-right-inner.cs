using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class leftrightinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inner",
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
                    table.PrimaryKey("PK_Inner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Left",
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
                    table.PrimaryKey("PK_Left", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Right",
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
                    table.PrimaryKey("PK_Right", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inner");

            migrationBuilder.DropTable(
                name: "Left");

            migrationBuilder.DropTable(
                name: "Right");
        }
    }
}
