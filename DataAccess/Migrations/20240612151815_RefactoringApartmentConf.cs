using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringApartmentConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apartments_Name",
                table: "Apartments");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Name_Price_MaxGuests_CityCountryId",
                table: "Apartments",
                columns: new[] { "Name", "Price", "MaxGuests", "CityCountryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apartments_Name_Price_MaxGuests_CityCountryId",
                table: "Apartments");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Name",
                table: "Apartments",
                column: "Name");
        }
    }
}
