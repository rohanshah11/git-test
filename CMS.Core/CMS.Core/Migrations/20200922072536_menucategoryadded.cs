using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class menucategoryadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menu_menu_type_menu_type_id",
                table: "menu");

            migrationBuilder.RenameColumn(
                name: "menu_type_id",
                table: "menu",
                newName: "menu_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_menu_menu_type_id",
                table: "menu",
                newName: "IX_menu_menu_category_id");

            migrationBuilder.AddColumn<long>(
                name: "MenuTypemenu_type_id",
                table: "menu",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_menu_MenuTypemenu_type_id",
                table: "menu",
                column: "MenuTypemenu_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_menu_menu_type_MenuTypemenu_type_id",
                table: "menu",
                column: "MenuTypemenu_type_id",
                principalTable: "menu_type",
                principalColumn: "menu_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_menu_category_menu_category_id",
                table: "menu",
                column: "menu_category_id",
                principalTable: "menu_category",
                principalColumn: "menu_category_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menu_menu_type_MenuTypemenu_type_id",
                table: "menu");

            migrationBuilder.DropForeignKey(
                name: "FK_menu_menu_category_menu_category_id",
                table: "menu");

            migrationBuilder.DropIndex(
                name: "IX_menu_MenuTypemenu_type_id",
                table: "menu");

            migrationBuilder.DropColumn(
                name: "MenuTypemenu_type_id",
                table: "menu");

            migrationBuilder.RenameColumn(
                name: "menu_category_id",
                table: "menu",
                newName: "menu_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_menu_menu_category_id",
                table: "menu",
                newName: "IX_menu_menu_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_menu_menu_type_menu_type_id",
                table: "menu",
                column: "menu_type_id",
                principalTable: "menu_type",
                principalColumn: "menu_type_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
