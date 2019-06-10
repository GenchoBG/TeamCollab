using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class CardArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Cards",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Cards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedDate",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ArchivedDate",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
