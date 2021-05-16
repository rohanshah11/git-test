using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class statuscreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_cancelled",
                table: "appointments");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "appointments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "appointments");

            migrationBuilder.AddColumn<bool>(
                name: "is_cancelled",
                table: "appointments",
                nullable: false,
                defaultValue: false);
        }
    }
}
