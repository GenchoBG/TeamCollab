using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Board_Projects_ProjectId",
                table: "Board");

            migrationBuilder.DropForeignKey(
                name: "FK_Board_Card_RootCardId",
                table: "Board");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Board_BoardId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_AspNetUsers_LastModifiedById",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Card_NextCardId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Card_PrevCardId",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Board",
                table: "Board");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "Board",
                newName: "Boards");

            migrationBuilder.RenameIndex(
                name: "IX_Card_PrevCardId",
                table: "Cards",
                newName: "IX_Cards_PrevCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_NextCardId",
                table: "Cards",
                newName: "IX_Cards_NextCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_LastModifiedById",
                table: "Cards",
                newName: "IX_Cards_LastModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Card_BoardId",
                table: "Cards",
                newName: "IX_Cards_BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Board_RootCardId",
                table: "Boards",
                newName: "IX_Boards_RootCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Board_ProjectId",
                table: "Boards",
                newName: "IX_Boards_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Projects_ProjectId",
                table: "Boards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Cards_RootCardId",
                table: "Boards",
                column: "RootCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_AspNetUsers_LastModifiedById",
                table: "Cards",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Cards_NextCardId",
                table: "Cards",
                column: "NextCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Cards_PrevCardId",
                table: "Cards",
                column: "PrevCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Projects_ProjectId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Cards_RootCardId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Boards_BoardId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_LastModifiedById",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Cards_NextCardId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Cards_PrevCardId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "Board");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_PrevCardId",
                table: "Card",
                newName: "IX_Card_PrevCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_NextCardId",
                table: "Card",
                newName: "IX_Card_NextCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_LastModifiedById",
                table: "Card",
                newName: "IX_Card_LastModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_BoardId",
                table: "Card",
                newName: "IX_Card_BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_RootCardId",
                table: "Board",
                newName: "IX_Board_RootCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_ProjectId",
                table: "Board",
                newName: "IX_Board_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Board",
                table: "Board",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Board_Projects_ProjectId",
                table: "Board",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Board_Card_RootCardId",
                table: "Board",
                column: "RootCardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Board_BoardId",
                table: "Card",
                column: "BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_AspNetUsers_LastModifiedById",
                table: "Card",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Card_NextCardId",
                table: "Card",
                column: "NextCardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Card_PrevCardId",
                table: "Card",
                column: "PrevCardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
