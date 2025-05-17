using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteApartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Apartments",
                type: "decimal(9,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lattitude",
                table: "Apartments",
                type: "decimal(9,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsArchived",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteApartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteApartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteApartments_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteApartments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteApartments_ApartmentId",
                table: "FavoriteApartments",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteApartments_UserId",
                table: "FavoriteApartments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteApartments");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Apartments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lattitude",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldNullable: true);
        }
    }
}
