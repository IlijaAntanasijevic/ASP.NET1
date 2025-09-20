using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OpenAi_AddUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OpenAiMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenAiMessages_UserId",
                table: "OpenAiMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAiMessages_Users_UserId",
                table: "OpenAiMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAiMessages_Users_UserId",
                table: "OpenAiMessages");

            migrationBuilder.DropIndex(
                name: "IX_OpenAiMessages_UserId",
                table: "OpenAiMessages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OpenAiMessages");
        }
    }
}
