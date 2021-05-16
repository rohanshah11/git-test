using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class doctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    doctor_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 250, nullable: false),
                    speciality = table.Column<string>(maxLength: 2000, nullable: true),
                    address = table.Column<string>(maxLength: 1000, nullable: true),
                    education = table.Column<string>(maxLength: 1000, nullable: true),
                    contact_number = table.Column<string>(maxLength: 50, nullable: true),
                    image = table.Column<string>(maxLength: 120, nullable: true),
                    is_contact_enabled = table.Column<bool>(nullable: false),
                    email = table.Column<string>(maxLength: 200, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    website = table.Column<string>(maxLength: 200, nullable: true),
                    facebook = table.Column<string>(maxLength: 200, nullable: true),
                    twitter = table.Column<string>(maxLength: 200, nullable: true),
                    experience = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.doctor_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctors");
        }
    }
}
