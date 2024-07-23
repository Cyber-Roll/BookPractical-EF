using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_DbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class createItemGenreJoinTableAndRelationships2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemGenre_Genres_GenreId",
                table: "ItemGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGenre_Items_ItemId",
                table: "ItemGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemGenre",
                table: "ItemGenre");

            migrationBuilder.RenameTable(
                name: "ItemGenre",
                newName: "ItemGenres");

            migrationBuilder.RenameIndex(
                name: "IX_ItemGenre_ItemId",
                table: "ItemGenres",
                newName: "IX_ItemGenres_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemGenre_GenreId",
                table: "ItemGenres",
                newName: "IX_ItemGenres_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemGenres",
                table: "ItemGenres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGenres_Genres_GenreId",
                table: "ItemGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGenres_Items_ItemId",
                table: "ItemGenres",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemGenres_Genres_GenreId",
                table: "ItemGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGenres_Items_ItemId",
                table: "ItemGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemGenres",
                table: "ItemGenres");

            migrationBuilder.RenameTable(
                name: "ItemGenres",
                newName: "ItemGenre");

            migrationBuilder.RenameIndex(
                name: "IX_ItemGenres_ItemId",
                table: "ItemGenre",
                newName: "IX_ItemGenre_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemGenres_GenreId",
                table: "ItemGenre",
                newName: "IX_ItemGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemGenre",
                table: "ItemGenre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGenre_Genres_GenreId",
                table: "ItemGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGenre_Items_ItemId",
                table: "ItemGenre",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
