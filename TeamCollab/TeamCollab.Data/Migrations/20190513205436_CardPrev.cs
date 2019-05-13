using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class CardPrev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrevCardId",
                table: "Card",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_PrevCardId",
                table: "Card",
                column: "PrevCardId",
                unique: true,
                filter: "[PrevCardId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Card_PrevCardId",
                table: "Card",
                column: "PrevCardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Card_PrevCardId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_PrevCardId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "PrevCardId",
                table: "Card");
        }
    }
}
