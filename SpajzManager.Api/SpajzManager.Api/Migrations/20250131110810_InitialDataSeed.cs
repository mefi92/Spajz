using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpajzManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Households",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Szülői ház", "Városlődi kecó" },
                    { 2, "Albérlet", "Palotai kégli" },
                    { 3, "Nyaraló", "Györöki kisház" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "HouseholdId", "Name" },
                values: new object[,]
                {
                    { 1, "gyümölcs", 1, "Alma" },
                    { 2, "egyenesen a tehénből", 1, "Tej" },
                    { 3, "utlimate zöldség", 1, "Brokkoli" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Households",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Households",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Households",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
