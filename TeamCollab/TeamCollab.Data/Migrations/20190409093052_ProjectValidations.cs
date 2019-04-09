using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class ProjectValidations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "Projects",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);
        }
    }
}
