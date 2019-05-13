using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCollab.Data.Migrations
{
    public partial class OneToOneFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_LastModifiedById",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_ProjectId",
                table: "Boards");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_LastModifiedById",
                table: "Cards",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ProjectId",
                table: "Boards",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_LastModifiedById",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_ProjectId",
                table: "Boards");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_LastModifiedById",
                table: "Cards",
                column: "LastModifiedById",
                unique: true,
                filter: "[LastModifiedById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ProjectId",
                table: "Boards",
                column: "ProjectId",
                unique: true);
        }
    }
}
