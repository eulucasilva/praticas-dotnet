using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddRelationMovieArtits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistMovie_Artist_ArtistsId",
                table: "ArtistMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistMovie_Movie_MoviesId",
                table: "ArtistMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistMovie",
                table: "ArtistMovie");

            migrationBuilder.RenameTable(
                name: "ArtistMovie",
                newName: "MovieArtists");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistMovie_MoviesId",
                table: "MovieArtists",
                newName: "IX_MovieArtists_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieArtists",
                table: "MovieArtists",
                columns: new[] { "ArtistsId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieArtists_Artist_ArtistsId",
                table: "MovieArtists",
                column: "ArtistsId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieArtists_Movie_MoviesId",
                table: "MovieArtists",
                column: "MoviesId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieArtists_Artist_ArtistsId",
                table: "MovieArtists");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieArtists_Movie_MoviesId",
                table: "MovieArtists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieArtists",
                table: "MovieArtists");

            migrationBuilder.RenameTable(
                name: "MovieArtists",
                newName: "ArtistMovie");

            migrationBuilder.RenameIndex(
                name: "IX_MovieArtists_MoviesId",
                table: "ArtistMovie",
                newName: "IX_ArtistMovie_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistMovie",
                table: "ArtistMovie",
                columns: new[] { "ArtistsId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistMovie_Artist_ArtistsId",
                table: "ArtistMovie",
                column: "ArtistsId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistMovie_Movie_MoviesId",
                table: "ArtistMovie",
                column: "MoviesId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
