using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class corepartofappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "doctors",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    appointment_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 2000, nullable: true),
                    address = table.Column<string>(maxLength: 2000, nullable: true),
                    contact_no = table.Column<string>(maxLength: 100, nullable: true),
                    description = table.Column<string>(maxLength: 2000, nullable: true),
                    email = table.Column<string>(maxLength: 250, nullable: true),
                    appointment_date = table.Column<DateTime>(nullable: false),
                    entry_date = table.Column<DateTime>(nullable: false),
                    country = table.Column<int>(nullable: false),
                    is_cancelled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.appointment_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "doctors",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
