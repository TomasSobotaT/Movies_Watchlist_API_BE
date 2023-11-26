using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_Watchlist_DB.Migrations
{
    /// <inheritdoc />
    public partial class Migrace2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Name", "csfdUrl", "posterUrl" },
                values: new object[] { 1, "Rambo", "www.csfd.cz", "https://www.movieposters.com/cdn/shop/products/firstblood24_480x.progressive.jpg?v=1626714427" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
