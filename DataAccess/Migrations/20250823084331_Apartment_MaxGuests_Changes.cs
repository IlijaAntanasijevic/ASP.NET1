using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Apartment_MaxGuests_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Apartments_Name_Price_MaxGuests_CityCountryId",
            //    table: "Apartments");

            migrationBuilder.AddColumn<int>(
                name: "MaxAdults",
                table: "Apartments",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxChildren",
                table: "Apartments",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRooms",
                table: "Apartments",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Name_Price_MaxAdults_MaxChildren_TotalRooms_CityCountryId",
                table: "Apartments",
                columns: new[] { "Name", "Price", "MaxAdults", "MaxChildren", "TotalRooms", "CityCountryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apartments_Name_Price_MaxAdults_MaxChildren_TotalRooms_CityCountryId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "MaxAdults",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "MaxChildren",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "TotalRooms",
                table: "Apartments");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Name_Price_MaxGuests_CityCountryId",
                table: "Apartments",
                columns: new[] { "Name", "Price", "MaxGuests", "CityCountryId" });
        }
    }
}
