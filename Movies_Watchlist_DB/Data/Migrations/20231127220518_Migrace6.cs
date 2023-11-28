using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_Watchlist_DB.Migrations
{
    /// <inheritdoc />
    public partial class Migrace6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Movies");

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

            migrationBuilder.CreateTable(
                name: "TestDeletedMovies",
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
                    table.PrimaryKey("PK_TestDeletedMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestMovies",
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
                    table.PrimaryKey("PK_TestMovies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TestMovies",
                columns: new[] { "Id", "CsfdUrl", "Name", "PosterUrl" },
                values: new object[] { 200, "test", "test", "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedMovies");

            migrationBuilder.DropTable(
                name: "TestDeletedMovies");

            migrationBuilder.DropTable(
                name: "TestMovies");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CsfdUrl", "Discriminator", "Name", "PosterUrl" },
                values: new object[] { 200, "test", "TestMovie", "test", "test" });
        }
    }
}
