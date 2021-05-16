using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class menuremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "menu_category");

            migrationBuilder.DropColumn(
                name: "image_name",
                table: "menu_category");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "menu_category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "menu_category",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_name",
                table: "menu_category",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "parent_id",
                table: "menu_category",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
