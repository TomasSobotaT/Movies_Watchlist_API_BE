using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_Watchlist_DB.Migrations
{
    /// <inheritdoc />
    public partial class Migrace5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CsfdUrl", "Discriminator", "Name", "PosterUrl" },
                values: new object[] { 200, "test", "TestMovie", "test", "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 200);
        }
    }
}
