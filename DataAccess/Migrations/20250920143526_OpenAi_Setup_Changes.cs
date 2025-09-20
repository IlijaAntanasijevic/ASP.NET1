using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OpenAi_Setup_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "OpenAiConversation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OpenAiConversation_ModelId",
                table: "OpenAiConversation",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAiConversation_OpenAiSetup_ModelId",
                table: "OpenAiConversation",
                column: "ModelId",
                principalTable: "OpenAiSetup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAiConversation_OpenAiSetup_ModelId",
                table: "OpenAiConversation");

            migrationBuilder.DropIndex(
                name: "IX_OpenAiConversation_ModelId",
                table: "OpenAiConversation");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "OpenAiConversation");
        }
    }
}
