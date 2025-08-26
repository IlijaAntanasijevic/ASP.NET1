using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmailConfirmations_Recreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmation_Users_UserId",
                table: "EmailConfirmation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailConfirmation",
                table: "EmailConfirmation");

            migrationBuilder.RenameTable(
                name: "EmailConfirmation",
                newName: "EmailConfirmations");

            migrationBuilder.RenameIndex(
                name: "IX_EmailConfirmation_UserId",
                table: "EmailConfirmations",
                newName: "IX_EmailConfirmations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailConfirmations",
                table: "EmailConfirmations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmations_Users_UserId",
                table: "EmailConfirmations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmations_Users_UserId",
                table: "EmailConfirmations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailConfirmations",
                table: "EmailConfirmations");

            migrationBuilder.RenameTable(
                name: "EmailConfirmations",
                newName: "EmailConfirmation");

            migrationBuilder.RenameIndex(
                name: "IX_EmailConfirmations_UserId",
                table: "EmailConfirmation",
                newName: "IX_EmailConfirmation_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailConfirmation",
                table: "EmailConfirmation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmation_Users_UserId",
                table: "EmailConfirmation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
