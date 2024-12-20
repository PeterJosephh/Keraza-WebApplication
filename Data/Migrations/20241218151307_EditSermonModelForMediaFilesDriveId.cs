using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KerazaWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditSermonModelForMediaFilesDriveId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioDriveId",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfDriveId",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoDriveId",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioDriveId",
                table: "Sermons");

            migrationBuilder.DropColumn(
                name: "PdfDriveId",
                table: "Sermons");

            migrationBuilder.DropColumn(
                name: "VideoDriveId",
                table: "Sermons");
        }
    }
}
