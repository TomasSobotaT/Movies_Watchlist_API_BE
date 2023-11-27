using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_Watchlist_DB.Migrations
{
    /// <inheritdoc />
    public partial class Migrace3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "posterUrl",
                table: "Movies",
                newName: "PosterUrl");

            migrationBuilder.RenameColumn(
                name: "csfdUrl",
                table: "Movies",
                newName: "CsfdUrl");

            migrationBuilder.CreateTable(
                name: "DeletedMovies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CsfdUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedMovies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedMovies");

            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "Movies",
                newName: "posterUrl");

            migrationBuilder.RenameColumn(
                name: "CsfdUrl",
                table: "Movies",
                newName: "csfdUrl");

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Name", "csfdUrl", "posterUrl" },
                values: new object[] { 1, "Rambo", "www.csfd.cz", "https://www.movieposters.com/cdn/shop/products/firstblood24_480x.progressive.jpg?v=1626714427" });
        }
    }
}
