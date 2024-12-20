using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KerazaWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_CaptionProp_inSermonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Sermons");
        }
    }
}
