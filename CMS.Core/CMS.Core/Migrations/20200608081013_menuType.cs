using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class menuType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu_type",
                columns: table => new
                {
                    menu_type_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 150, nullable: false),
                    menu_category_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_type", x => x.menu_type_id);
                    table.ForeignKey(
                        name: "FK_menu_type_menu_category_menu_category_id",
                        column: x => x.menu_category_id,
                        principalTable: "menu_category",
                        principalColumn: "menu_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_type_menu_category_id",
                table: "menu_type",
                column: "menu_category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_type");
        }
    }
}
