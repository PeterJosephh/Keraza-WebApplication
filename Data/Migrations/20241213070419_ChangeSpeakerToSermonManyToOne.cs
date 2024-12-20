using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KerazaWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSpeakerToSermonManyToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sermons_SpeakerId",
                table: "Sermons");

            migrationBuilder.CreateIndex(
                name: "IX_Sermons_SpeakerId",
                table: "Sermons",
                column: "SpeakerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sermons_SpeakerId",
                table: "Sermons");

            migrationBuilder.CreateIndex(
                name: "IX_Sermons_SpeakerId",
                table: "Sermons",
                column: "SpeakerId",
                unique: true);
        }
    }
}
