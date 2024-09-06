using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RiverBooks.Books.data.migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Books");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Books",
                table: "Books",
                columns: new[] { "Id", "Author", "Price", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "JB Peterson", 20.00m, "12 Rules for Life", 2018 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "JB Peterson", 25.00m, "Beyond Order", 2021 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "JB Peterson", 30.00m, "Maps of Meaning", 1999 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "Books");
        }
    }
}
