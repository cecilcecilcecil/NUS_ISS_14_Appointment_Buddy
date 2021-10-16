using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HSPSpecialist.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Specialist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NRIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services = table.Column<int>(type: "int", nullable: false),
                    Contact = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialist", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate" },
                values: new object[] { 3, "S1234567C", new DateTime(2021, 10, 17, 1, 29, 2, 454, DateTimeKind.Local).AddTicks(2556), "testServices", false, "S234567C", new DateTime(2021, 10, 17, 1, 29, 2, 454, DateTimeKind.Local).AddTicks(2980) });

            migrationBuilder.InsertData(
                table: "Specialist",
                columns: new[] { "Id", "Address", "Available", "Contact", "CreatedBy", "CreatedDate", "Email", "IsDeleted", "LastUpdatedBy", "LastUpdatedDate", "NRIC", "Name", "ServiceDescription", "Services" },
                values: new object[] { 1, "BLK221 PENDING ROAD #08-149", true, 7697807, "S1234567C", new DateTime(2021, 10, 17, 1, 29, 2, 452, DateTimeKind.Local).AddTicks(2545), "test@singnet.com", false, "S234567C", new DateTime(2021, 10, 17, 1, 29, 2, 453, DateTimeKind.Local).AddTicks(1245), "S1234567C", "Milo", null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Specialist");
        }
    }
}
