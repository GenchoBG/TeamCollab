using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class LastEditedByCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Card",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_LastModifiedById",
                table: "Card",
                column: "LastModifiedById",
                unique: true,
                filter: "[LastModifiedById] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_AspNetUsers_LastModifiedById",
                table: "Card",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_AspNetUsers_LastModifiedById",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_LastModifiedById",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Card");
        }
    }
}
