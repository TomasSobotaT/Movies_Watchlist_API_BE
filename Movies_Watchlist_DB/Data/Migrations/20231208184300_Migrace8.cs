using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_Watchlist_DB.Migrations
{
    /// <inheritdoc />
    public partial class Migrace8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestMovies",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DropColumn(
                name: "CsfdUrl",
                table: "TestMovies");

            migrationBuilder.DropColumn(
                name: "CsfdUrl",
                table: "TestDeletedMovies");

            migrationBuilder.DropColumn(
                name: "CsfdUrl",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CsfdUrl",
                table: "DeletedMovies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CsfdUrl",
                table: "TestMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CsfdUrl",
                table: "TestDeletedMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CsfdUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CsfdUrl",
                table: "DeletedMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "TestMovies",
                columns: new[] { "Id", "CsfdUrl", "DateAdded", "Name", "PosterUrl" },
                values: new object[] { 200, "test", new DateTime(2023, 12, 3, 15, 15, 17, 978, DateTimeKind.Local).AddTicks(7285), "test", "test" });
        }
    }
}
