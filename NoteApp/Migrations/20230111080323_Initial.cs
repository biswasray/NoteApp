using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Body = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Note",
                columns: new[] { "Id", "Body", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[] { "3f57e70f-13d2-4720-93b9-9d8b11c34b69", "Widely regarded as \"The Oldest Military Treatise in the World,\" this landmark work covers principles of strategy, tactics, maneuvering, communication, and supplies; the use of terrain, fire, and the seasons of the year; the classification ...", new DateTime(2023, 1, 11, 13, 33, 23, 111, DateTimeKind.Local).AddTicks(4495), "The Art of War", new DateTime(2023, 1, 11, 13, 33, 23, 111, DateTimeKind.Local).AddTicks(4547) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note");
        }
    }
}
