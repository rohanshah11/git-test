using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class menuDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    menu_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    menu_type_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 150, nullable: false),
                    description = table.Column<string>(maxLength: 2000, nullable: false),
                    unit_price = table.Column<decimal>(nullable: false),
                    image_name = table.Column<string>(nullable: false),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.menu_id);
                    table.ForeignKey(
                        name: "FK_menu_menu_type_menu_type_id",
                        column: x => x.menu_type_id,
                        principalTable: "menu_type",
                        principalColumn: "menu_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_menu_type_id",
                table: "menu",
                column: "menu_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu");
        }
    }
}
