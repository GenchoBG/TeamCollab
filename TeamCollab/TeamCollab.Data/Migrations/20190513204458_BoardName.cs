using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class BoardName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Board",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Board");
        }
    }
}
