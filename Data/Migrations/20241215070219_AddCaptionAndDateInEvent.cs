using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KerazaWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCaptionAndDateInEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Events");
        }
    }
}
