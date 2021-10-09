using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HSPService.Migrations
{
    public partial class intitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "Services" },
                values: new object[] { 1, "S1234567C", new DateTime(2021, 9, 11, 22, 41, 32, 378, DateTimeKind.Local).AddTicks(3649), false, "S234567C", new DateTime(2021, 9, 11, 22, 41, 32, 379, DateTimeKind.Local).AddTicks(3669), "testServices" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
