using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KerazaWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class addImageURLForSpeakerAndSermon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Sermons");
        }
    }
}
