using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OpenAi_Setup_ChangeToSetupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAiConversation_OpenAiSetup_ModelId",
                table: "OpenAiConversation");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "OpenAiConversation",
                newName: "SetupId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenAiConversation_ModelId",
                table: "OpenAiConversation",
                newName: "IX_OpenAiConversation_SetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAiConversation_OpenAiSetup_SetupId",
                table: "OpenAiConversation",
                column: "SetupId",
                principalTable: "OpenAiSetup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAiConversation_OpenAiSetup_SetupId",
                table: "OpenAiConversation");

            migrationBuilder.RenameColumn(
                name: "SetupId",
                table: "OpenAiConversation",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenAiConversation_SetupId",
                table: "OpenAiConversation",
                newName: "IX_OpenAiConversation_ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAiConversation_OpenAiSetup_ModelId",
                table: "OpenAiConversation",
                column: "ModelId",
                principalTable: "OpenAiSetup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
